using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// Tag API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork"></param>
        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET list of tags
        /// </summary>
        /// <returns>List of tags</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Tags.Get());
        }

        /// <summary>
        /// GET tag by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>TagDM</returns>
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

        /// <summary>
        /// POST tag
        /// </summary>
        /// <param name="tag">TagDM</param>
        /// <returns>Saved or updated TagDM</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(TagDM)))]
        public IActionResult Post([FromBody] TagDM tag)
        {
            if (tag == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Tags.AddOrUpdate(tag);
            _unitOfWork.Save();

            return Ok(tag);
        }

        /// <summary>
        /// PUT TagDM
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="tag">TagDM</param>
        /// <returns>Updated TagDM</returns>
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

        /// <summary>
        /// DELETE TagDM
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>Deleted TagDM</returns>
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
