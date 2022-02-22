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
    public class RolePermissionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolePermissionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RolePermission)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.RolePermissions.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RolePermission)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get(string id)
        {
            var rolePermission = _unitOfWork.RolePermissions.GetById(id);
            if (rolePermission == null)
                return NotFound();
            else
                return Ok(rolePermission);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RolePermission)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] RolePermission rolePermission)
        {
            if (rolePermission == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RolePermissions.Add(rolePermission);
            _unitOfWork.Save();

            return Ok(rolePermission);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RolePermission)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Put(int id, [FromBody] RolePermission rolePermission)
        {
            if (rolePermission == null)
                return BadRequest();

            if (id != rolePermission.RolePermissionId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RolePermissions.Update(rolePermission);
            _unitOfWork.Save();

            return Ok(rolePermission);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RolePermission)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Delete(string id)
        {
            var rolePermission = _unitOfWork.RolePermissions.GetById(id);
            if (rolePermission == null)
                return NotFound();

            _unitOfWork.UserClaims.Delete(rolePermission);
            _unitOfWork.Save();

            return Ok(rolePermission);
        }
    }
}
