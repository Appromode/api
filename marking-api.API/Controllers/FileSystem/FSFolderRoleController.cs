using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFolderRoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFolderRoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderRoleDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFolderRoles.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderRoleDM)))]
        public IActionResult Get(long id)
        {
            var folderRole = _unitOfWork.FSFolderRoles.GetById(id);
            if (folderRole == null)
                return NotFound();
            else
                return Ok(folderRole);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderRoleDM)))]
        public IActionResult Post([FromBody] FSFolderRoleDM folderRole)
        {
            if (folderRole == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolderRoles.Update(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderRoleDM)))]
        public IActionResult Put(long id, [FromBody] FSFolderRoleDM folderRole)
        {
            if (folderRole == null)
                return BadRequest();

            if (id != folderRole.FolderRoleId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFolderRoles.Update(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFolderRoleDM)))]
        public IActionResult Delete(long id)
        {
            var folderRole = _unitOfWork.FSFolderRoles.GetById(id);
            if (folderRole == null)
                return NotFound();

            folderRole.deleted = true;
            _unitOfWork.FSFolderRoles.Update(folderRole);
            _unitOfWork.Save();

            return Ok(folderRole);
        }
    }
}
