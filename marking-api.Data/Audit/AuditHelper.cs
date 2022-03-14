using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.Data.Audit
{
    /// <summary>
    /// Class that contains the method to add audit logs for each modified 
    /// </summary>
    public class AuditHelper
    {
        //Audit database interface
        private readonly IAuditDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">Injected audit database context</param>
        public AuditHelper(IAuditDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Converts all changes detected by the ChangeTracker to audit logs with details about the change
        /// </summary>
        /// <param name="userId">Userid of the user currently using the application / altering the database</param>
        public void AddLogs(string userId)
        {
            _context.ChangeTracker.DetectChanges();
            List<AuditDetails> entries = new List<AuditDetails>();
            foreach (EntityEntry entry in _context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged
                    || entry.State == EntityState.Unchanged)
                {
                    continue;
                }
                var auditEntry = new AuditDetails(entry, userId);
                entries.Add(auditEntry);
            }

            if (entries.Any())
            {
                var logs = entries.Select(x => x.ToAudit());
                _context.Audits.AddRange(logs);
            }
        }
    }
}
