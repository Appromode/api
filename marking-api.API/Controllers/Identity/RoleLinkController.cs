using log4net.Core;
using marking_api.API.Config;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleLinkController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleLinkController(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleLinkDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.RoleLinks.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleLinkDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get(Int64 id)
        {
            var roleLink = _unitOfWork.RoleLinks.GetById(id);
            if (roleLink == null)
                return NotFound();
            else
                return Ok(roleLink);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleLinkDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] RoleLinkDM roleLink)
        {
            if (roleLink == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RoleLinks.AddOrUpdate(roleLink);
            _unitOfWork.Save();

            return Ok(roleLink);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleLinkDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Put(Int64 id, [FromBody] RoleLinkDM roleLink)
        {
            if (roleLink == null)
                return BadRequest();

            if (id != roleLink.RoleLinkId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.RoleLinks.Update(roleLink);
            _unitOfWork.Save();

            return Ok(roleLink);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(RoleLinkDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Delete(Int64 id)
        {
            var roleLink = _unitOfWork.RoleLinks.GetById(id);
            if (roleLink == null)
                return NotFound();

            _unitOfWork.RoleLinks.Delete(roleLink);
            _unitOfWork.Save();

            return Ok(roleLink);
        }
    }
}
