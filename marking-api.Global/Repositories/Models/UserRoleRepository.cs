using marking_api.Data;
using marking_api.DataModel.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserRoleRepository : IGenericModelRepository<UserRole>
    {

    }
    public class UserRoleRepository : GenericModelRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
