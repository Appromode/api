using marking_api.Data;
using Microsoft.AspNetCore.Identity;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IRoleClaimRepository : IGenericModelRepository<IdentityRoleClaim<string>>
    {

    }
    public class RoleClaimRepository : GenericModelRepository<IdentityRoleClaim<string>>, IRoleClaimRepository
    {
        public RoleClaimRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
