using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserRoleRepository : IGenericModelRepository<UserRole>
    {

    }
    public class UserRoleRepository : GenericModelRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
