using marking_api.DataModel.Identity;
using marking_api.DataModel.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace marking_api.Data
{
    public class MarkingDbContext: IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IWebHostEnvironment env;
        public string UserId { get; set; }

        public MarkingDbContext(DbContextOptions<MarkingDbContext> options, IWebHostEnvironment env) :base(options)
        {
            this.env = env;
        }

        public MarkingDbContext()
        {

        }

        //Identity
        public DbSet<User> IdUsers { get; set; }
        public DbSet<UserClaim> IdUserClaims { get; set; }
        public DbSet<UserToken> IdUserTokens { get; set; }
        public DbSet<UserLogin> IdUserLogins { get; set; }
        public DbSet<Role> IdRoles { get; set; }
        public DbSet<RoleClaim> IdRoleClaims { get; set; }

        //File System
        public DbSet<FSFileDM> FSFiles { get; set; }
        public DbSet<FSFileStateDM> FSFileStates { get; set; }
        public DbSet<FSFileVersionDM> FSFileVersions { get; set; }
        public DbSet<FSFolderDM> FSFolders { get; set; }
        public DbSet<FSFolderFileDM> FSFolderFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<User>(entity => 
            { 
                entity.ToTable(name: "IdUsers", schema: "dbo");
                entity.Property(e => e.Id).HasColumnName("UserId");
                entity.Property(e => e.ProfilePicture).HasColumnName("ProfilePicture");

                entity.HasMany(e => e.UserClaims)
                .WithOne(x => x.User)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

                entity.HasMany(e => e.UserLogins)
                .WithOne(x => x.User)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

                entity.HasMany(e => e.UserTokens)
                .WithOne(x => x.User)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

                entity.HasMany(e => e.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(a => a.UserId)
                .IsRequired();
            });

            builder.Entity<UserRole>(entity => 
            {
                entity.ToTable(name: "IdUserRoles", schema: "dbo");                
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            builder.Entity<UserClaim>(entity =>
            {
                entity.ToTable(name: "IdUserClaims", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<UserLogin>(entity =>
            {
                entity.ToTable(name: "IdUserLogins", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<UserToken>(entity =>
            {
                entity.ToTable(name: "IdUserTokens", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "IdRoles", schema: "dbo");
                entity.Property(e => e.Id).HasColumnName("RoleId");

                entity.HasMany(e => e.RoleClaims)
                .WithOne(x => x.Role)
                .HasForeignKey(a => a.RoleId)
                .IsRequired();
            });

            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "IdRoleClaims", schema: "dbo");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            //Example override for view
            //If There is a single table that the view originates from then use the primary key from that table otherwise no.
            //The binding model would go in marking-api.DataModel
            //builder.Entity<ModelToBindViewTo>().ToView(nameof(ViewName)).Has(No)Key();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) 
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
