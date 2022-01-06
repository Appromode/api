using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Comments.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Get(long id)
        {
            var comment = _unitOfWork.Comments.GetById(id);
            if (comment == null)
                return NotFound();
            else
                return Ok(comment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Post([FromBody] CommentDM comment)
        {
            if (comment == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Comments.AddOrUpdate(comment);
            _unitOfWork.Save();

            return Ok(comment);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Put(long id, [FromBody] CommentDM comment)
        {
            if (comment == null)
                return BadRequest();

            if (id != comment.CommentId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Comments.Update(comment);
            _unitOfWork.Save();

            return Ok(comment);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Delete(long id)
        {
            var comment = _unitOfWork.Comments.GetById(id);
            if (comment == null)
                return NotFound();

            comment.deleted = true;
            _unitOfWork.Comments.Update(comment);
            _unitOfWork.Save();

            return Ok(comment);
        }
    }
}
