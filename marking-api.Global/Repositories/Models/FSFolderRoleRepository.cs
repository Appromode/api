using marking_api.Data;
using marking_api.DataModel.FileSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IFSFolderRoleRepository : IGenericModelRepository<FSFolderRoleDM> { }

    public class FSFolderRoleRepository : GenericModelRepository<FSFolderRoleDM>, IFSFolderRoleRepository
    {
        public FSFolderRoleRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
