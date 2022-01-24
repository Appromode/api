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

        public void Migrate()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }
        }

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
            }
        }
    }
}
