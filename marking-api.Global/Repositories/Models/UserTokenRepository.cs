using marking_api.Data;
using Microsoft.AspNetCore.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserTokenRepository : IGenericModelRepository<IdentityUserToken<string>>
    {

    }
    public class UserTokenRepository : GenericModelRepository<IdentityUserToken<string>>, IUserTokenRepository
    {
        public UserTokenRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
