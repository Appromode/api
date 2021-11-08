using marking_api.Data;
using marking_api.DataModel.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserRepository : IGenericModelRepository<User>
    {

    }
    public class UserRepository : GenericModelRepository<User>, IUserRepository
    {
        public UserRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
