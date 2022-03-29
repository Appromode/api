using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marking_api.API.Models
{
    public class BaseModel
    {
        private ILog _logger;
        public BaseModel(ILog logger)
        {
            _logger = logger;
        }
    }
}
