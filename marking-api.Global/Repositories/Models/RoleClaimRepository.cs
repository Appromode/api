using marking_api.Data;
using Microsoft.AspNetCore.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IRoleClaimRepository : IGenericModelRepository<IdentityRoleClaim<string>>
    {

    }
    public class RoleClaimRepository : GenericModelRepository<IdentityRoleClaim<string>>, IRoleClaimRepository
    {
        public RoleClaimRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
