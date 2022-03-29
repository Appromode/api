using marking_api.Data;
using marking_api.Global.Services;
using Microsoft.AspNetCore.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserTokenRepository : IGenericModelRepository<IdentityUserToken<string>>
    {

    }
    public class UserTokenRepository : GenericModelRepository<IdentityUserToken<string>>, IUserTokenRepository
    {
        public UserTokenRepository(MarkingDbContext dbContext, DataFilterService dfService) : base(dbContext, dfService) { }
    }
}
