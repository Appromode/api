using marking_api.Data;
using Microsoft.AspNetCore.Identity;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserLoginRepository : IGenericModelRepository<IdentityUserLogin<string>> 
    {
        
    }

    public class UserLoginRepository : GenericModelRepository<IdentityUserLogin<string>>, IUserLoginRepository
    {
        public UserLoginRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
