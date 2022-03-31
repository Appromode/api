using marking_api.DataModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Data
{
    /// <summary>
    /// Database seeder class. Seeds data into the database using the database context
    /// </summary>
    public class MarkingDbSeeder
    {
        //Database context
        private readonly MarkingDbContext _dbContext;
        //Identity sign in manager
        private readonly SignInManager<User> _signInManager;
        //Bool for if database is already seeded
        private bool _seeded = false;

        private byte[] profilePic = Encoding.ASCII.GetBytes("iVBORw0KGgoAAAANSUhEUgAAADIAAAAyBAMAAADsEZWCAAAAElBMVEXR0dGqqqrCwsKysrK5ubnKysqGpJ8RAAAAtUlEQVQ4y72TMQ7CMAxFTaAH+CnsKRI7QepOBvZWgvtfBaESSv1jKRKob32K/W238iOP40lKuATAC9NEvLiy6TBxoSdvwa+2WVCrER+USbMJSxNhNcLMeW3D2UwTzEkHWnXG3FsrtdVg760qdW22nXltSRSNyolmVIPyZ+XJNOo6HGEQo9xeLOP/ZDZmH9t0VmqHiZ5WkNThWKBdKBfxxa1wUP3n34GycmD6vH4m5CbEIc/OyBPDUSvwZuB80QAAAABJRU5ErkJggg");

        /// <summary>
        /// Database seeder constructor
        /// </summary>
        /// <param name="dbContext">Database context</param>
        /// <param name="signInManager">Identity signinmanager</param>
        public MarkingDbSeeder(MarkingDbContext dbContext, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Upon project startup will check and execute any outstanding migrations against the database
        /// </summary>
        public void Migrate()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }
        }

        /// <summary>
        /// On project startup will add data to the database. If the database has already been created and seed then this method will be skipped
        /// </summary>
        public async void SeedData()
        {
            if (!_seeded)
            {
                var existing = await _signInManager.UserManager.FindByNameAsync("sjp75");

                //If the passwordhash of the above user is not null then the database has already been seeded
                //If not then the hash will be generated and extra users seeded
                if (existing.PasswordHash != null)
                {
                    Console.WriteLine("Database already seeded!");
                    _seeded = true;
                    return;
                }

                User user = _dbContext.IdUsers.FirstOrDefault(x => x.Email.Equals("sjp75@kent.ac.uk"));
                user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, "Tester!2");
                user.ConcurrencyStamp = Guid.NewGuid().ToString();
                user.SecurityStamp = Guid.NewGuid().ToString();
                user.ProfilePicture = profilePic;

                var x = _dbContext.IdUsers.Find(user.Id);
                _dbContext.Entry(x).CurrentValues.SetValues(user);
                _dbContext.SaveChanges();

                SeedUsers();
                SeedLinks();
                SeedRoles(user);
            }
        }

        /// <summary>
        /// Seeds extra users within the database on creation of the database.
        /// </summary>
        public void SeedUsers()
        {
            User user1 = new User
            {
                Id = Convert.ToString(Guid.NewGuid()),
                FirstName = "Not A",
                LastName = "User",
                UserName = "NotAUser",
                NormalizedUserName = "NOTAUSER",
                Email = "NotAUser@StillNotAUser.com",
                NormalizedEmail = "NOTAUSER@STILLNOTAUSER.COM",
                EmailConfirmed = true,
                IsDisabled = false,
                IsDeleted = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = Convert.ToString(Guid.NewGuid()),
                ConcurrencyStamp = Convert.ToString(Guid.NewGuid()),
                ProfilePicture = profilePic,
                AccessFailedCount = 0,
                TwoFactorEnabled = false
            };            
            _dbContext.Users.Add(user1);
            _dbContext.SaveChanges();
            user1.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user1, "Tester!2");
            _dbContext.Users.Update(user1);
            _dbContext.SaveChanges();

            User user2 = new User
            {
                Id = Convert.ToString(Guid.NewGuid()),
                FirstName = "Probably Not A",
                LastName = "User",
                UserName = "ProbablyNotAUser",
                NormalizedUserName = "PROBABLYNOTAUSER",
                Email = "ProbablyNotAUser@StillNotAUser.com",
                NormalizedEmail = "PROBABLYAUSER@STILLNOTAUSER.COM",
                EmailConfirmed = true,
                IsDisabled = false,
                IsDeleted = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = Convert.ToString(Guid.NewGuid()),
                ConcurrencyStamp = Convert.ToString(Guid.NewGuid()),
                ProfilePicture = profilePic,
                AccessFailedCount = 0,
                TwoFactorEnabled = false
            };            
            _dbContext.Users.Add(user2);
            _dbContext.SaveChanges();
            user2.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user2, "Tester!2");
            _dbContext.Users.Update(user2);
            _dbContext.SaveChanges();

            User user3 = new User
            {
                Id = Convert.ToString(Guid.NewGuid()),
                FirstName = "Definitely Not A",
                LastName = "User",
                UserName = "DefinitelyNotAUser",
                NormalizedUserName = "DEFINITELYNOTAUSER",
                Email = "DefinitelyNotAUser@StillNotAUser.com",
                NormalizedEmail = "DEFINITELYNOTAUSER@STILLNOTAUSER.COM",
                EmailConfirmed = true,
                IsDisabled = false,
                IsDeleted = false,
                LockoutEnabled = false,
                LockoutEnd = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = Convert.ToString(Guid.NewGuid()),
                ConcurrencyStamp = Convert.ToString(Guid.NewGuid()),
                ProfilePicture = profilePic,
                AccessFailedCount = 0,
                TwoFactorEnabled = false
            };
            _dbContext.Users.Add(user3);
            _dbContext.SaveChanges();
            user3.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user3, "Tester!2");
            _dbContext.Users.Update(user3);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Seed menu links into the database on initial database creation
        /// </summary>
        public void SeedLinks()
        {
            var link = new LinkDM
            {
                LinkName = "Project Forum",
                LinkUrl = "/project-forum",
                LinkPosition = 0,
                LinkSecurity = "project forum"
            };
            _dbContext.Links.Add(link);
            

            link = new LinkDM
            {
                LinkName = "Project Supervisors",
                LinkUrl = "/project-supervisors",
                LinkPosition = 1,
                LinkSecurity = "project security"
            };
            _dbContext.Links.Add(link);

            link = new LinkDM
            {
                LinkName = "Dashboard",
                LinkUrl = "/dashboard",
                LinkPosition = 2,
                LinkSecurity = "dashboard"
            };
            _dbContext.Links.Add(link);

            var config = new LinkDM
            {
                LinkName = "Config",
                LinkPosition = 3,
            };
            _dbContext.Links.Add(config);
            _dbContext.SaveChanges();

            link = new LinkDM
            {
                LinkName = "Query",
                LinkUrl = "/config/query",
                LinkPosition = 0,
                LinkSecurity = "query",
                LinkParentId = config.LinkId
            };
            _dbContext.Links.Add(link);

            link = new LinkDM
            {
                LinkName = "Users",
                LinkUrl = "/config/users",
                LinkPosition = 1,
                LinkSecurity = "users",
                LinkParentId = config.LinkId
            };
            _dbContext.Links.Add(link);

            link = new LinkDM
            {
                LinkName = "Roles",
                LinkUrl = "/config/roles",
                LinkPosition = 2,
                LinkSecurity = "roles",
                LinkParentId = config.LinkId
            };
            _dbContext.Links.Add(link);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Seed roles and role links into the database
        /// </summary>
        public void SeedRoles(User user)
        {
            var role = new Role
            {
                Name = "Guest",
                NormalizedName = "GUEST",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleDescription = "Guest Role"
            };
            _dbContext.Roles.Add(role);

            var student = new Role
            {
                Name = "Student",
                NormalizedName = "STUDENT",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleDescription = "Student Role"
            };
            _dbContext.Roles.Add(student);

            role = new Role
            {
                Name = "Lecturer",
                NormalizedName = "LECTURER",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleDescription = "Lecturer Role"
            };
            _dbContext.Roles.Add(role);

            var admin = new Role
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleDescription = "Admin Role"
            };
            _dbContext.Roles.Add(admin);
            _dbContext.SaveChanges();

            var userrole = new UserRole
            {
                RoleId = admin.Id,
                UserId = user.Id
            };
            _dbContext.UserRoles.Add(userrole);

            List<User> users = _dbContext.Users.Where(x => x.Email != "sjp75@kent.ac.uk").ToList();
            foreach (var testuser in users)
            {
                userrole = new UserRole
                {
                    RoleId = student.Id,
                    UserId = testuser.Id
                };
            }
            _dbContext.SaveChanges();

            List<LinkDM> links = _dbContext.Links.ToList();

            foreach (var link in links)
            {
                _dbContext.RoleLinks.Add(new RoleLinkDM
                {
                    LinkId = link.LinkId,
                    RoleId = admin.Id
                });
                if (!link.LinkUrl.Contains("config"))
                {
                    _dbContext.RoleLinks.Add(new RoleLinkDM
                    {
                        LinkId = link.LinkId,
                        RoleId = student.Id
                    });
                }
                
            }
            _dbContext.SaveChanges();
        }
    }
}
