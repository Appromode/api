﻿using marking_api.Data;
using marking_api.DataModel.Logging;
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
        public LogRepository(MarkingDbContext dbContext) : base(dbContext) { }
    }
}
