using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ThreadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Threads.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Get(long id)
        {
            var grade = _unitOfWork.Threads.GetById(id);
            if (grade == null)
                return NotFound();
            else
                return Ok(grade);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Post([FromBody] ThreadDM thread)
        {
            if (thread == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Threads.AddOrUpdate(thread);
            _unitOfWork.Save();

            return Ok(thread);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Put(long id, [FromBody] ThreadDM thread)
        {
            if (thread == null)
                return BadRequest();

            if (id != thread.ThreadId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Threads.Update(thread);
            _unitOfWork.Save();

            return Ok(thread);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Delete(long id)
        {
            var thread = _unitOfWork.Threads.GetById(id);
            if (thread == null)
                return NotFound();

            thread.deleted = true;
            _unitOfWork.Threads.Update(thread);
            _unitOfWork.Save();

            return Ok(thread);
        }
    }
}
