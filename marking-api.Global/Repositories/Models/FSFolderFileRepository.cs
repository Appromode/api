using marking_api.Data;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFolderFileRepository : IGenericModelRepository<FSFolderFileDM>
    {

    }
    public class FSFolderFileRepository : GenericModelRepository<FSFolderFileDM>, IFSFolderFileRepository
    {
        public FSFolderFileRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
