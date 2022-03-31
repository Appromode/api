using log4net;
using marking_api.Data;
using marking_api.DataModel.Logging;
using marking_api.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.Global.Repositories.Models
{
    public interface ILogRepository : IGenericModelRepository<LogDM> { }

    public class LogRepository : GenericModelRepository<LogDM>, ILogRepository
    {
        public LogRepository(MarkingDbContext dbContext, DataFilterService dfService, ILog logger) : base(dbContext, dfService, logger) { }
    }
}
