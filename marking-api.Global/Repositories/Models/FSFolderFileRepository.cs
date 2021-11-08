using marking_api.Data;
using marking_api.DataModel.FileSystem;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFolderFileRepository : IGenericModelRepository<FSFolderFileDM>
    {

    }
    public class FSFolderFileRepository : GenericModelRepository<FSFolderFileDM>, IFSFolderFileRepository
    {
        public FSFolderFileRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
