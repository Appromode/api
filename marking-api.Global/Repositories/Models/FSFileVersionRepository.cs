using marking_api.Data;
using marking_api.DataModel.FileSystem;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFileVersionRepository : IGenericModelRepository<FSFileVersionDM>
    {

    }
    public class FSFileVersionRepository : GenericModelRepository<FSFileVersionDM>, IFSFileVersionRepository
    {
        public FSFileVersionRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
