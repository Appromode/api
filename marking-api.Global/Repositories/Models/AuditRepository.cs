using marking_api.Data;
using marking_api.DataModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IAuditRepository : IGenericModelRepository<AuditDM> { }

    public class AuditRepository : GenericModelRepository<AuditDM>, IAuditRepository
    {
        public AuditRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
