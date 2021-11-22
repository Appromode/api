using marking_api.Data;
using marking_api.DataModel.Identity;
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
        public RolePermissionRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
