using marking_api.Data;
using marking_api.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IRoleLinkRepository : IGenericModelRepository<RoleLinkDM> { }

    public class RoleLinkRepository : GenericModelRepository<RoleLinkDM>, IRoleLinkRepository
    {
        public RoleLinkRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
