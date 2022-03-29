using log4net.Core;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }        
    }
}
