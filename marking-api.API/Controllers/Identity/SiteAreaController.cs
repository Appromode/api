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
    public class SiteAreaController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public SiteAreaController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(SiteArea)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.SiteAreas.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(SiteArea)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get(long id)
        {
            var siteArea = _unitOfWork.SiteAreas.GetById(id);
            if (siteArea == null)
                return NotFound();
            else
                return Ok(siteArea);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(SiteArea)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] SiteArea siteArea)
        {
            if (siteArea == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.SiteAreas.AddOrUpdate(siteArea);
            _unitOfWork.Save();

            return Ok(siteArea);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(SiteArea)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Put(long id, [FromBody] SiteArea siteArea)
        {
            if (siteArea == null)
                return BadRequest();

            if (id != siteArea.SiteAreaId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.SiteAreas.Update(siteArea);
            _unitOfWork.Save();

            return Ok(siteArea);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(SiteArea)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Delete(string id)
        {
            var siteArea = _unitOfWork.SiteAreas.GetById(id);
            if (siteArea == null)
                return NotFound();

            siteArea.deleted = true;
            _unitOfWork.SiteAreas.Update(siteArea);
            _unitOfWork.Save();

            return Ok(siteArea);
        }
    }
}
