using marking_api.DataModel.Identity;
using marking_api.DataModel.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using marking_api.DataModel.Project;
using marking_api.DataModel.API;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using marking_api.Data.Audit;
using marking_api.DataModel.Logging;

namespace marking_api.Data
{
    public class MarkingDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IMapDbContext
    {
        private readonly IWebHostEnvironment _env;

        public string UserId { get; set; }

        public MarkingDbContext(DbContextOptions<MarkingDbContext> options, IWebHostEnvironment env) :base(options)
        {
            _env = env;
        }

        public MarkingDbContext()
        {

        }

        //Identity Database Tables
        public DbSet<User> IdUsers { get; set; }
        public DbSet<UserClaim> IdUserClaims { get; set; }
        public DbSet<UserToken> IdUserTokens { get; set; }
        public DbSet<UserLogin> IdUserLogins { get; set; }
        public DbSet<Role> IdRoles { get; set; }
        public DbSet<RoleClaim> IdRoleClaims { get; set; }
        public DbSet<RolePermission> IdRolePermissions { get; set; }
        public DbSet<SiteArea> IdSiteAreas { get; set; }

        //File System Database Tables
        public DbSet<FSFileDM> FSFiles { get; set; }
        public DbSet<FSFileStateDM> FSFileStates { get; set; }
        public DbSet<FSFileVersionDM> FSFileVersions { get; set; }
        public DbSet<FSFolderDM> FSFolders { get; set; }
        public DbSet<FSFolderFileDM> FSFolderFiles { get; set; }
        public DbSet<FSFolderRoleDM> FSFolderRoles { get; set; }

        //Project Database Tables
        public DbSet<CommentDM> Comments { get; set; }
        public DbSet<GradeDM> Grades { get; set; }
        public DbSet<GroupDM> Groups { get; set; }
        public DbSet<GroupMarkerDM> GroupMarkers { get; set; }
        public DbSet<FeedbackDM> Feedback { get; set; }
        public DbSet<ProjectDM> Projects { get; set; }
        public DbSet<TagDM> Tags { get; set; }
        public DbSet<ThreadDM> Threads { get; set; }
        public DbSet<UserGradeDM> UserGrades { get; set; }
        public DbSet<UserGroupDM> UserGroups { get; set; }
        public DbSet<UserTagsDM> UserTags { get; set; }

        //API Database Tables
        public DbSet<RefreshTokenDM> RefreshTokens { get; set; }

        //Logging Database Tables
        public DbSet<AuditDM> Audits { get; set; }
        public DbSet<LogDM> Logs { get; set; }

        //Config Database Tables
        public DbSet<LinkDM> Links { get; set; }
        public DbSet<RoleLinkDM> RoleLinks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ServerVersion.AutoDetect(configuration.GetConnectionString("DbConnection")));
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

                entity.HasMany(e => e.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(a => a.RoleId)
                .IsRequired();
            });

            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "IdRoleClaims", schema: "dbo");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            builder.Entity<TagDM>(entity =>
            {
                entity.HasIndex(i => i.TagName).IsUnique(true);
            });

            builder.Entity<GroupDM>(entity =>
            {
                entity.HasIndex(i => i.GroupName).IsUnique(true);
            });

            builder.Entity<RefreshTokenDM>(entity =>
            {
                entity.ToTable(name: "RefreshTokens", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<AuditDM>(entity =>
            {
                entity.ToTable(name: "Audit", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<GradeDM>(entity =>
            {
                entity.ToTable(name: "Grades", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            builder.Entity<FeedbackDM>(entity =>
            {
                entity.ToTable(name: "Feedback", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            //Example override for view
            //If There is a single table that the view originates from then use the primary key from that table otherwise no.
            //The binding model would go in marking-api.DataModel
            //builder.Entity<ModelToBindViewTo>().ToView(nameof(ViewName)).Has(No)Key();
        }

        public override int SaveChanges()
        {
            new AuditHelper(this).AddLogs(UserId);
            ChangeTracker.DetectChanges();
            foreach(var entry in ChangeTracker.Entries())
            {
                Type t = entry.Entity.GetType();
                if (entry.State == EntityState.Added)
                {
                    if (t.GetProperty("createdAt") != null)
                    {
                        var field = entry.Entity.GetType().GetProperty("createdAt");
                        field.SetValue(entry.Entity, DateTime.Now);
                        field = entry.Entity.GetType().GetProperty("updatedAt");                        
                        field.SetValue(entry.Entity, DateTime.Now);
                    }
                } else if (entry.State == EntityState.Modified)
                {
                    if (t.GetProperty("updatedAt") != null)
                    {
                        var field = entry.Entity.GetType().GetProperty("updatedAt");
                        field.SetValue(entry.Entity, DateTime.Now);
                    }
                }              
            }
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) 
        {
            new AuditHelper(this).AddLogs(UserId);
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
            {
                Type t = entry.Entity.GetType();
                if (entry.State == EntityState.Added)
                {
                    if (t.GetProperty("createdAt") != null)
                    {
                        var field = entry.Entity.GetType().GetProperty("createdAt");
                        field.SetValue(entry.Entity, DateTime.Now);
                        field = entry.Entity.GetType().GetProperty("updatedAt");
                        field.SetValue(entry.Entity, DateTime.Now);
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (t.GetProperty("updatedAt") != null)
                    {
                        var field = entry.Entity.GetType().GetProperty("updatedAt");
                        field.SetValue(entry.Entity, DateTime.Now);
                    }
                }
            }
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
