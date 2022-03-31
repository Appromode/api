using log4net;
using marking_api.Data;
using marking_api.DataModel.Project;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface IUserGroupRepository : IGenericModelRepository<UserGroupDM> { }

    public class UserGroupRepository : GenericModelRepository<UserGroupDM>, IUserGroupRepository
    {
        public UserGroupRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
