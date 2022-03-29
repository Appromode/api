using marking_api.Data;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFileRepository : IGenericModelRepository<FSFileDM>
    {

    }

    public class FSFileRepository : GenericModelRepository<FSFileDM>, IFSFileRepository
    {
        public FSFileRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
