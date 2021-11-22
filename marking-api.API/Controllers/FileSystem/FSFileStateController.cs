using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFileStateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFileStateController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileStateDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFileStates.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileStateDM)))]
        public IActionResult Get(long id)
        {
            var fileState = _unitOfWork.FSFileStates.GetById(id);
            if (fileState == null)
                return NotFound();
            else
                return Ok(fileState);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileStateDM)))]
        public IActionResult Post([FromBody] FSFileStateDM fileState)
        {
            if (fileState == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFileStates.Update(fileState);
            _unitOfWork.Save();

            return Ok(fileState);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileStateDM)))]
        public IActionResult Put(long id, [FromBody] FSFileStateDM fileState)
        {
            if (fileState == null)
                return BadRequest();

            if (id != fileState.FileStateId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFileStates.Update(fileState);
            _unitOfWork.Save();

            return Ok(fileState);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileStateDM)))]
        public IActionResult Delete(long id)
        {
            var fileState = _unitOfWork.FSFileStates.GetById(id);
            if (fileState == null)
                return NotFound();

            fileState.deleted = true;
            _unitOfWork.FSFileStates.Update(fileState);
            _unitOfWork.Save();

            return Ok(fileState);
        }
    }
}
