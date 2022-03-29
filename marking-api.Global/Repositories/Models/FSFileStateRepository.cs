using log4net;
using marking_api.Data;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFileStateRepository : IGenericModelRepository<FSFileStateDM>
    {

    }
    public class FSFileStateRepository : GenericModelRepository<FSFileStateDM>, IFSFileStateRepository
    {
        public FSFileStateRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
