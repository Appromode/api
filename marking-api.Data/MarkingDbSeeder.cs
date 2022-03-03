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
    public class MarkingDbSeeder
    {
        private readonly MarkingDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private bool _seeded = false;

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
        /// On projec startup will add data to the database.
        /// </summary>
        public async void SeedData()
        {
            if (!_seeded)
            {
                var existing = await _signInManager.UserManager.FindByNameAsync("sjp75");

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

                var x = _dbContext.IdUsers.Find(user.Id);
                _dbContext.Entry(x).CurrentValues.SetValues(user);
                _dbContext.SaveChanges();

                SeedUsers();
            }
        }

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
                ProfilePicture = null,
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
                ProfilePicture = null,
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
                ProfilePicture = null,
                AccessFailedCount = 0,
                TwoFactorEnabled = false
            };
            user3.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user3, "Tester!2");
            _dbContext.Users.Add(user3);
            _dbContext.SaveChanges();
        }
    }
}
