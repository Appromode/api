using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleClaimController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleClaimController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.RoleClaims.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
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
        public IActionResult Add([FromBody] RoleClaim roleClaim)
        {
            if (roleClaim == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RoleClaims.Add(roleClaim);
            _unitOfWork.Save();

            return Ok(roleClaim);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleClaim)))]
        public IActionResult Update(int id, [FromBody] RoleClaim roleClaim)
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