using log4net;
using marking_api.API.Config;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using marking_api.API.Models.FileSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.API;
using System.Linq;
using System.Collections.Generic;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFileVersionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFileVersionController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFileVersions.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Get(long id)
        {
            var fileVersion = _unitOfWork.FSFileVersions.GetById(id);
            if (fileVersion == null)
                return NotFound();
            else
                return Ok(fileVersion);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Post([FromBody] FileRequest fileRequest)
        {
            if (fileRequest.File == null || fileRequest.GroupId == 0 || fileRequest.UserId == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cm = new FileCM(_unitOfWork, _logger);

            Dictionary<FSFileDM, bool> result = cm.SaveFile(fileRequest);

            if (result.Values.FirstOrDefault() == true)
                return Ok(result.Keys.FirstOrDefault());
            else
                return BadRequest("An error occured saving the file");
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Put(long id, [FromBody] FSFileVersionDM fileVersion)
        {
            if (fileVersion == null)
                return BadRequest();

            if (id != fileVersion.FileVersionId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFileVersions.Update(fileVersion);
            _unitOfWork.Save();

            return Ok(fileVersion);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Delete(long id)
        {
            var fileVersion = _unitOfWork.FSFileVersions.GetById(id);
            if (fileVersion == null)
                return NotFound();

            fileVersion.deleted = true;
            _unitOfWork.FSFileVersions.Update(fileVersion);
            _unitOfWork.Save();

            return Ok(fileVersion);
        }
    }
}