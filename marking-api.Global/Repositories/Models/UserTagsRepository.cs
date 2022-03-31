using log4net;
using marking_api.Data;
using marking_api.DataModel.Project;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserTagRepository : IGenericModelRepository<UserTagsDM> { }

    public class UserTagRepository : GenericModelRepository<UserTagsDM>, IUserTagRepository
    {
        public UserTagRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
