using log4net;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using marking_api.API.Config;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleClaimController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleClaimController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.RoleClaims.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get(string id)
        {
            var roleClaim = _unitOfWork.RoleClaims.GetById(id);
            if (roleClaim == null)
                return NotFound();
            else
                return Ok(roleClaim);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] RoleClaim roleClaim)
        {
            if (roleClaim == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RoleClaims.AddOrUpdate(roleClaim);
            _unitOfWork.Save();

            return Ok(roleClaim);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Put(int id, [FromBody] RoleClaim roleClaim)
        {
            if (roleClaim == null)
                return BadRequest();

            if (id != roleClaim.Id)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RoleClaims.Update(roleClaim);
            _unitOfWork.Save();

            return Ok(roleClaim);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Delete(string id)
        {
            var roleClaim = _unitOfWork.RoleClaims.GetById(id);
            if (roleClaim == null)
                return NotFound();

            _unitOfWork.RoleClaims.Delete(roleClaim);
            _unitOfWork.Save();

            return Ok(roleClaim);
        }
    }
}