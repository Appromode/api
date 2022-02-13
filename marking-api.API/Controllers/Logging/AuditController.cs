using marking_api.DataModel.Logging;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Logging
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuditController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(AuditDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Audits.Get());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(AuditDM)))]
        public IActionResult Get(long id)
        {
            var audit = _unitOfWork.Audits.GetById(id);
            if (audit == null)
                return NotFound();
            else
                return Ok(audit);
        }
    }
}
