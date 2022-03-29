using log4net;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ILog _logger;

        public BaseController(ILog logger)
        {
            _logger = logger;
        }        
    }
}
