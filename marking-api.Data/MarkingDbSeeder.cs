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
        public MarkingDbSeeder(MarkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            if (_dbContext.Database.GetPendingMigrations().Any())
            {
                _dbContext.Database.Migrate();
            }
        }
    }
}
