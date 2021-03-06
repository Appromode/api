using log4net;
using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface ISiteAreaRepository : IGenericModelRepository<SiteArea>
    {

    }

    public class SiteAreaRepository : GenericModelRepository<SiteArea>, ISiteAreaRepository
    {
        public SiteAreaRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
