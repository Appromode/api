using log4net;
using marking_api.Data;
using Microsoft.AspNetCore.Identity;
using marking_api.Global.Services;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserLoginRepository : IGenericModelRepository<IdentityUserLogin<string>> 
    {
        
    }

    public class UserLoginRepository : GenericModelRepository<IdentityUserLogin<string>>, IUserLoginRepository
    {
        public UserLoginRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
