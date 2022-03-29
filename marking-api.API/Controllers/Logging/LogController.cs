using log4net;
using marking_api.API.Config;
using marking_api.DataModel.Logging;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Logging
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LogDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Logging")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Logs.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(LogDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Logging")]
        public IActionResult Get(long id)
        {
            var log = _unitOfWork.Logs.GetById(id);
            if (log == null)
                return NotFound();
            else
                return Ok(log);
        }
    }
}
