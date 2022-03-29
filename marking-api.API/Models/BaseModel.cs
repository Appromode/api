using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marking_api.API.Models
{
    public class BaseModel
    {
        private ILogger _logger;
        public BaseModel(ILogger logger)
        {
            _logger = logger;
        }
    }
}
