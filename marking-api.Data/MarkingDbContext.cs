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
    /// <summary>
    /// Application database context class
    /// </summary>
    public class MarkingDbContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IMapDbContext
    {
        //The environement the application is running in. Used to get details about how the application has been configured
        private readonly IWebHostEnvironment _env;

        ///The id of the user currently logged in and using the application
        public string UserId { get; set; }

        /// <summary>
        /// Database context to access the data in each table within the database
        /// </summary>
        /// <param name="options">DbContextOptions - Database context options to configure how the database is setup and operates</param>
        /// <param name="env">IWebHostEnvironment - Details about the environment the application is running in</param>
        public MarkingDbContext(DbContextOptions<MarkingDbContext> options, IWebHostEnvironment env) :base(options)
        {
            _env = env;
        }

        /// <summary>
        /// 
        /// </summary>
        public MarkingDbContext()
        {

        }

        //Identity Database Tables
        /// <summary>
        /// User Table
        /// </summary>
        public DbSet<User> IdUsers { get; set; }
        /// <summary>
        /// User Claims Table
        /// </summary>
        public DbSet<UserClaim> IdUserClaims { get; set; }
        /// <summary>
        /// User Tokens Table
        /// </summary>
        public DbSet<UserToken> IdUserTokens { get; set; }
        /// <summary>
        /// User Logins Table
        /// </summary>
        public DbSet<UserLogin> IdUserLogins { get; set; }
        /// <summary>
        /// Roles Table
        /// </summary>
        public DbSet<Role> IdRoles { get; set; }
        /// <summary>
        /// Role Claims Table
        /// </summary>
        public DbSet<RoleClaim> IdRoleClaims { get; set; }
        /// <summary>
        /// Role Permissions Table
        /// </summary>
        public DbSet<RolePermission> IdRolePermissions { get; set; }
        /// <summary>
        /// Site Areas Table
        /// </summary>
        public DbSet<SiteArea> IdSiteAreas { get; set; }

        //File System Database Tables
        /// <summary>
        /// File Table
        /// </summary>
        public DbSet<FSFileDM> FSFiles { get; set; }
        /// <summary>
        /// File State Table
        /// </summary>
        public DbSet<FSFileStateDM> FSFileStates { get; set; }
        /// <summary>
        /// File Version Table
        /// </summary>
        public DbSet<FSFileVersionDM> FSFileVersions { get; set; }
        /// <summary>
        /// Folder Table
        /// </summary>
        public DbSet<FSFolderDM> FSFolders { get; set; }
        /// <summary>
        /// Folder File Table
        /// </summary>
        public DbSet<FSFolderFileDM> FSFolderFiles { get; set; }
        /// <summary>
        /// Folder Role Table
        /// </summary>
        public DbSet<FSFolderRoleDM> FSFolderRoles { get; set; }

        //Project Database Tables
        /// <summary>
        /// Comments Table
        /// </summary>
        public DbSet<CommentDM> Comments { get; set; }
        /// <summary>
        /// Grades Table
        /// </summary>
        public DbSet<GradeDM> Grades { get; set; }
        /// <summary>
        /// Groups Table
        /// </summary>
        public DbSet<GroupDM> Groups { get; set; }
        /// <summary>
        /// Group Markers Table
        /// </summary>
        public DbSet<GroupMarkerDM> GroupMarkers { get; set; }
        /// <summary>
        /// Feedback Table
        /// </summary>
        public DbSet<FeedbackDM> Feedback { get; set; }
        /// <summary>
        /// Projects Table
        /// </summary>
        public DbSet<ProjectDM> Projects { get; set; }
        /// <summary>
        /// Tags Table
        /// </summary>
        public DbSet<TagDM> Tags { get; set; }
        /// <summary>
        /// Threads Table
        /// </summary>
        public DbSet<ThreadDM> Threads { get; set; }
        /// <summary>
        /// User Grades Table
        /// </summary>
        public DbSet<UserGradeDM> UserGrades { get; set; }
        /// <summary>
        /// User Groups Table
        /// </summary>
        public DbSet<UserGroupDM> UserGroups { get; set; }
        /// <summary>
        /// User Tags Table
        /// </summary>
        public DbSet<UserTagsDM> UserTags { get; set; }

        //API Database Tables
        /// <summary>
        /// Refresh Tokens Table
        /// </summary>
        public DbSet<RefreshTokenDM> RefreshTokens { get; set; }

        //Logging Database Tables
        /// <summary>
        /// Aduit Logs Table
        /// </summary>
        public DbSet<AuditDM> Audits { get; set; }
        /// <summary>
        /// Logs Table
        /// </summary>
        public DbSet<LogDM> Logs { get; set; }

        //Config Database Tables
        /// <summary>
        /// Menu Links Table
        /// </summary>
        public DbSet<LinkDM> Links { get; set; }
        /// <summary>
        /// Role Links Table
        /// </summary>
        public DbSet<RoleLinkDM> RoleLinks { get; set; }
        /// <summary>
        /// Invites Table
        /// </summary>
         public DbSet<InviteDM> Invites { get; set; } 

        /// <summary>
        /// Configures the DbContext
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder - Used for configuring options for the DbContext. Is injected by the application</param>
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

        /// <summary>
        /// Configures db entities and how they are created within the database
        /// </summary>
        /// <param name="builder">An interface for configuring database entities</param>
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

            //Rename User Id column to UserId in RoleClaims table
            builder.Entity<RoleClaim>(entity =>
            {
                entity.ToTable(name: "IdRoleClaims", schema: "dbo");
                entity.Property(e => e.RoleId).HasColumnName("RoleId");
            });

            //Make TagName unique in Tags table
            builder.Entity<TagDM>(entity =>
            {
                entity.HasIndex(i => i.TagName).IsUnique(true);
            });

            //Make GroupName unique in Group table
            builder.Entity<GroupDM>(entity =>
            {
                entity.HasIndex(i => i.GroupName).IsUnique(true);
            });

            //Rename User Id column to UserId in Refresh Tokens table
            builder.Entity<RefreshTokenDM>(entity =>
            {
                entity.ToTable(name: "RefreshTokens", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            //Rename User Id column to UserId in Audit table
            builder.Entity<AuditDM>(entity =>
            {
                entity.ToTable(name: "Audit", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            //Rename User Id column to UserId in Grades table
            builder.Entity<GradeDM>(entity =>
            {
                entity.ToTable(name: "Grades", schema: "dbo");
                entity.Property(e => e.UserId).HasColumnName("UserId");
            });

            //Rename User Id column to UserId in Feedback table
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


        /// <summary>
        /// Modifies the entities being tracked by the ChangeTracker to add details about who and when the entity was added / changed.
        /// Creates logs of details about the entity, who and when it was altered
        /// This is the main method that is called to save changes to the database. Every method within the UnitOfWork object uses this method and not SaveChangesAsync()
        /// </summary>
        /// <returns>Returns the base SaveChanges method once the entities have been modified</returns>
        public override int SaveChanges()
        {
            //Create audit logs for each entity tracked by the changetracker
            new AuditHelper(this).AddLogs(UserId);
            //Detect changes being tracked by entity framework
            ChangeTracker.DetectChanges();
            //Go through each entity and add who and when an entity was created, updated and soft deleted
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

        /// <summary>
        /// Asynchronously modifies the entities being tracked by the ChangeTracker to add details about who and when the entity was added / changed.
        /// Creates logs of details about the entity, who and when it was altered
        /// </summary>
        /// <param name="cancellationToken">Used in case the task is cancelled which will throw an exception</param>
        /// <returns>SaveChangesAsync base method once modifications to entities have happened</returns>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) 
        {
            //Create audit logs for each entity tracked by the changetracker
            new AuditHelper(this).AddLogs(UserId);
            //Detect changes being tracked by entity framework
            ChangeTracker.DetectChanges();
            //Go through each entity and add who and when an entity was created, updated and soft deleted
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
