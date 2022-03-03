using marking_api.DataModel.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Data.Audit
{
    public interface IAuditDbContext
    {
        DbSet<AuditDM> Audits { get; set; }
        ChangeTracker ChangeTracker { get; }
    }
}
