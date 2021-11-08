using marking_api.Data;
using marking_api.DataModel.FileSystem;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFileRepository : IGenericModelRepository<FSFileDM>
    {

    }

    public class FSFileRepository : GenericModelRepository<FSFileDM>, IFSFileRepository
    {
        public FSFileRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
