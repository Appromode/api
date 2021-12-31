using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFileVersionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFileVersionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFileVersions.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        public IActionResult Get(long id)
        {
            var fileVersion = _unitOfWork.FSFileVersions.GetById(id);
            if (fileVersion == null)
                return NotFound();
            else
                return Ok(fileVersion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
        public IActionResult Post([FromBody] FSFileVersionDM fileVersion)
        {
            if (fileVersion == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFileVersions.AddOrUpdate(fileVersion);
            _unitOfWork.Save();

            return Ok(fileVersion);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileVersionDM)))]
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