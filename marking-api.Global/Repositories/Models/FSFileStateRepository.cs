using marking_api.Data;
using marking_api.DataModel.FileSystem;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFileStateRepository : IGenericModelRepository<FSFileStateDM>
    {

    }
    public class FSFileStateRepository : GenericModelRepository<FSFileStateDM>, IFSFileStateRepository
    {
        public FSFileStateRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
