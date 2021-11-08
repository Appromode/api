using marking_api.Data;
using Microsoft.AspNetCore.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserClaimRepository : IGenericModelRepository<IdentityUserClaim<string>>
    {

    }
    public class UserClaimRepository : GenericModelRepository<IdentityUserClaim<string>>, IUserClaimRepository
    {
        public UserClaimRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
