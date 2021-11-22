using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Tags.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Get(long id)
        {
            var tag = _unitOfWork.Tags.GetById(id);
            if (tag == null)
                return NotFound();
            else
                return Ok(tag);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Post([FromBody] TagDM tag)
        {
            if (tag == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Tags.Update(tag);
            _unitOfWork.Save();

            return Ok(tag);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Put(long id, [FromBody] TagDM tag)
        {
            if (tag == null)
                return BadRequest();

            if (id != tag.TagId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Tags.Update(tag);
            _unitOfWork.Save();

            return Ok(tag);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Delete(long id)
        {
            var tag = _unitOfWork.Tags.GetById(id);
            if (tag == null)
                return NotFound();

            tag.deleted = true;
            _unitOfWork.Tags.Update(tag);
            _unitOfWork.Save();

            return Ok(tag);
        }
    }
}
