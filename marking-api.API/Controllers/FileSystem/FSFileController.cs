using marking_api.DataModel.FileSystem;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.FileSystem
{
    [ApiController]
    [Route("api/[controller]")]
    public class FSFileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public FSFileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.FSFiles.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileDM)))]
        public IActionResult Get(long id)
        {
            var file = _unitOfWork.FSFiles.GetById(id);
            if (file == null)
                return NotFound();
            else
                return Ok(file);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileDM)))]
        public IActionResult Post([FromBody] FSFileDM file)
        {
            if (file == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFiles.AddOrUpdate(file);
            _unitOfWork.Save();

            return Ok(file);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileDM)))]
        public IActionResult Put(long id, [FromBody] FSFileDM file)
        {
            if (file == null)
                return BadRequest();

            if (id != file.FileId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.FSFiles.Update(file);
            _unitOfWork.Save();

            return Ok(file);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FSFileDM)))]
        public IActionResult Delete(long id)
        {
            var file = _unitOfWork.FSFiles.GetById(id);
            if (file == null)
                return NotFound();

            file.deleted = true;
            _unitOfWork.FSFiles.Update(file);
            _unitOfWork.Save();

            return Ok(file);
        }
    }
}
