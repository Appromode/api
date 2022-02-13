using marking_api.Data;
using marking_api.DataModel.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IRefreshTokenRepository : IGenericModelRepository<RefreshTokenDM> { }

    public class RefreshTokenRepository : GenericModelRepository<RefreshTokenDM>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
