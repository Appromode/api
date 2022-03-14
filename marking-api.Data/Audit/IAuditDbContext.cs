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
    /// <summary>
    /// Interface for audit database context
    /// </summary>
    public interface IAuditDbContext
    {
        /// <summary>
        /// Access to the audit table
        /// </summary>
        DbSet<AuditDM> Audits { get; set; }
        /// <summary>
        /// ChangeTracker which detects modifications to entities being tracked by entity framework
        /// </summary>
        ChangeTracker ChangeTracker { get; }
    }
}
