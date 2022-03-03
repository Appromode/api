using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace marking_api.Data.Audit
{
    public class AuditHelper
    {
        private readonly IAuditDbContext _context;

        public AuditHelper(IAuditDbContext context)
        {
            _context = context;
        }

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
