using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IRolePermissionRepository : IGenericModelRepository<RolePermission> { }

    class RolePermissionRepository : GenericModelRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
