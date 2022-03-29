using log4net;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// Mark API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MarkController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public MarkController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET FeedbackDMs
        /// </summary>
        /// <returns>List of FeedbackDMs</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FeedbackDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Feedback.Get());
        }

        /// <summary>
        /// GET FeedbackDM by id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>FeedbackDM</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FeedbackDM)))]
        public IActionResult Get(long id)
        {
            var mark = _unitOfWork.Feedback.GetById(id);
            if (mark == null)
                return NotFound();
            else
                return Ok(mark);
        }

        /// <summary>
        /// POST FeedbackDM
        /// </summary>
        /// <param name="mark">FeedbackDM</param>
        /// <returns>Saved FeedbackDM</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FeedbackDM)))]
        public IActionResult Post([FromBody] FeedbackDM mark)
        {
            if (mark == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Feedback.AddOrUpdate(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }

        /// <summary>
        /// PUT FeedbackDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="mark">FeedbackDM</param>
        /// <returns>Updated FeedbackDM</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FeedbackDM)))]
        public IActionResult Put(long id, [FromBody] FeedbackDM mark)
        {
            if (mark == null)
                return BadRequest();

            if (id != mark.FeedbackId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Feedback.Update(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }

        /// <summary>
        /// DELETE FeedbackDM
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>Deleted FeedbackDM</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(FeedbackDM)))]
        public IActionResult Delete(long id)
        {
            var mark = _unitOfWork.Feedback.GetById(id);
            if (mark == null)
                return NotFound();

            mark.deleted = true;
            _unitOfWork.Feedback.Update(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }
    }
}
