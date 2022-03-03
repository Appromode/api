using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Data.Audit
{
    public interface IMapDbContext : IAuditDbContext, IDisposable
    {
        DatabaseFacade Database { get; }
        int SaveChanges();
    }
}
