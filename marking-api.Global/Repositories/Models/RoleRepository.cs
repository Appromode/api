using marking_api.Data;
using marking_api.DataModel.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IRoleRepository : IGenericModelRepository<Role>
    {

    }
    public class RoleRepository : GenericModelRepository<Role>, IRoleRepository
    {
        public RoleRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
