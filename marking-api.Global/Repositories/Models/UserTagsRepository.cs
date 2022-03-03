using marking_api.Data;
using marking_api.DataModel.Project;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserTagRepository : IGenericModelRepository<UserTagsDM> { }

    public class UserTagRepository : GenericModelRepository<UserTagsDM>, IUserTagRepository
    {
        public UserTagRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
