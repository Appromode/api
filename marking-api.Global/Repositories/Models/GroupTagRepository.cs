using log4net;
using marking_api.Data;
using marking_api.DataModel.Project;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IGroupTagRepository : IGenericModelRepository<GroupTagDM> { }

    public class GroupTagRepository : GenericModelRepository<GroupTagDM>, IGroupTagRepository
    {
        public GroupTagRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
