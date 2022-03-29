using marking_api.Data;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFolderRepository : IGenericModelRepository<FSFolderDM>
    {

    }

    public class FSFolderRepository : GenericModelRepository<FSFolderDM>, IFSFolderRepository
    {
        public FSFolderRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
