using System;
using log4net.Core;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// Thread API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ThreadController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ThreadController(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET threads
        /// </summary>
        /// <returns>List of threads</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Threads.Get(
                include: (thread) => thread.Include((thread) => thread.User)
            ));
        }

        /// <summary>
        /// GET ThreadDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>ThreadDM</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Get(long id)
        {
            var thread = _unitOfWork.Threads.Get(
                include: (thread) => thread.Include((thread) => thread.User).Include((thread) => thread.LinkedProject),
                filter: (thread) => thread.ThreadId == id
            );

            if (thread == null)
                return NotFound();
            else
                return Ok(thread);
        }

        /// <summary>
        /// GET ThreadDM including comments, user, quoted comment
        /// </summary>
        /// <param name="threadId"></param>
        /// <returns>ThreadDM</returns>
        [HttpGet("{threadId}/comments")]
         [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Get(int threadId)
        {
            var threadComments = _unitOfWork.Threads.Get(
                include: (threadDM) => threadDM
                    .Include((threadDM) => threadDM.Comments)
                    .ThenInclude((threadComments) => threadComments.QuotedComment)
                    .ThenInclude((threadComments) => threadComments.User)
                    .Include((threadDM) => threadDM.Comments)
                    .ThenInclude((threadComments) => threadComments.User),
                filter: (threadDM) => threadDM.ThreadId == threadId);

            return Ok(threadComments);
        }

        /// <summary>
        /// POST ThreadDM
        /// </summary>
        /// <param name="thread">ThreadDM</param>
        /// <returns>Saved or updated ThreadDM</returns>
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

        /// <summary>
        /// PUT ThreadDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="thread">ThreadDM</param>
        /// <returns>Updated ThreadDM</returns>
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

        /// <summary>
        /// DELETE ThreadDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>Deleted ThreadDM</returns>
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

        /// <summary>
        /// PATCH ThreadDM by id. Updates single value / only certain values
        /// instead of the whole object.
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="patchEntity">JsonPatchDocument(ThreadDM)</param>
        /// <returns>Updated ThreadDM</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        public IActionResult Patch(long id, [FromBody] JsonPatchDocument<ThreadDM> patchEntity)
        {
            if (patchEntity != null)
            {
                var thread = _unitOfWork.Threads.GetById(id);

                patchEntity.ApplyTo(thread, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                _unitOfWork.Threads.Update(thread);
                _unitOfWork.Save();
                return Ok(thread);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[HttpGet("WholeThread/{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(ThreadDM)))]
        //public IActionResult WholeThread(long id)
        //{
        //    return Ok(_unitOfWork.Threads.GetById(id, include: x => x.Include(x => x.LinkedProject).Include(x => x.Comments).ThenInclude(x => x.User).Include(x => x.User)));
        //}
    }
}
