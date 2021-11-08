using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marking_api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : ControllerBase where T : class
    {
        protected IGenericModelRepository<T> _genericModelRepository;
        protected IGenericViewRepository<T> _genericViewRepository;

        public BaseController(IGenericModelRepository<T> genericModelRepository, IGenericViewRepository<T> genericViewRepository)
        {
            _genericModelRepository = genericModelRepository;
            _genericViewRepository = genericViewRepository;
        }
    }
}
