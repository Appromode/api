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
            user1.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user1, "Tester!2");
            _dbContext.Users.Add(user1);
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
            user2.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user2, "Tester!2");
            _dbContext.Users.Add(user2);
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
            user3.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user3, "Tester!2");
            _dbContext.Users.Add(user3);
            _dbContext.SaveChanges();
        }
    }
}
