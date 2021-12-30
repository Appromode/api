using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFolderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFolderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFolders.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderDM)))]
        public IActionResult Get(long id)
        {
            var folderRole = _unitOfWork.FSFolders.GetById(id);
            if (folderRole == null)
                return NotFound();
            else
                return Ok(folderRole);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderDM)))]
        public IActionResult Post([FromBody] FSFolderDM folderRole)
        {
            if (folderRole == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolders.AddOrUpdate(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderDM)))]
        public IActionResult Put(long id, [FromBody] FSFolderDM folderRole)
        {
            if (folderRole == null)
                return BadRequest();

            if (id != folderRole.FolderId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolders.Update(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderDM)))]
        public IActionResult Delete(long id)
        {
            var folderRole = _unitOfWork.FSFolders.GetById(id);
            if (folderRole == null)
                return NotFound();

            folderRole.deleted = true;
            _unitOfWork.FSFolders.Update(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }
    }
}