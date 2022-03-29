using log4net;
using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IRoleRepository : IGenericModelRepository<Role>
    {

    }
    public class RoleRepository : GenericModelRepository<Role>, IRoleRepository
    {
        public RoleRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
