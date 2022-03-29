using log4net.Core;
using marking_api.API.Config;
using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFolderFileController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFolderFileController(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderFileDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFolderFiles.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderFileDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Get(long id)
        {
            var folderFile = _unitOfWork.FSFolderFiles.GetById(id);
            if (folderFile == null)
                return NotFound();
            else
                return Ok(folderFile);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderFileDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Post([FromBody] FSFolderFileDM folderFile)
        {
            if (folderFile == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolderFiles.Add(folderFile);
            _unitOfWork.Save();

            return Ok(folderFile);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderFileDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Put(long id, [FromBody] FSFolderFileDM folderFile)
        {
            if (folderFile == null)
                return BadRequest();

            if (id != folderFile.FolderFileId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolderFiles.Update(folderFile);
            _unitOfWork.Save();

            return Ok(folderFile);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderFileDM)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "FileSystem")]
        public IActionResult Delete(long id)
        {
            var folderFile = _unitOfWork.FSFolderFiles.GetById(id);
            if (folderFile == null)
                return NotFound();

            folderFile.deleted = true;
            _unitOfWork.FSFolderFiles.Update(folderFile);
            _unitOfWork.Save();

            return Ok(folderFile);
        }
    }
}
