using marking_api.Data;
using marking_api.DataModel.Project;

namespace marking_api.Global.Repositories.Models
{
    public interface IGroupTagRepository : IGenericModelRepository<GroupTagDM> { }

    public class GroupTagRepository : GenericModelRepository<GroupTagDM>, IGroupTagRepository
    {
        public GroupTagRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
