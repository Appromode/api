using System;
using log4net;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// Comment API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public CommentController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get comments method
        /// </summary>
        /// <returns>List of comments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Comments.Get(
                include: (comment) => comment.Include((comment) => comment.QuotedComment)
            ));
        }

        /// <summary>
        /// Get comment by id method
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>CommentDM</returns>
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

        /// <summary>
        /// Post comment method to save in the database
        /// </summary>
        /// <param name="comment">CommentDM</param>
        /// <returns>Saved CommentDM</returns>
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

            var comentId = comment.CommentId;

            var commentThread = _unitOfWork.Comments.GetById(comentId);

            return Ok(_unitOfWork.Comments.Get(
            include: (comment) => comment.Include((comment) => comment.ParentThread),
            filter: (comment) => comment.CommentId == comentId
            ));

        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(CommentDM)))]
        public IActionResult Patch(long id, [FromBody] JsonPatchDocument<CommentDM> patchEntity)
        {
            if (patchEntity != null)
            {
                var comment = _unitOfWork.Comments.GetById(id);

                patchEntity.ApplyTo(comment, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                _unitOfWork.Comments.Update(comment);
                _unitOfWork.Save();
                return Ok(comment);
            }
            else
            {
                return BadRequest(ModelState);
            }
    }

        /// <summary>
        /// Put comment method by id
        /// </summary>
        /// <param name="id">long</param>
        /// <param name="comment">CommentDM</param>
        /// <returns>Saved CommentDM</returns>
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

        /// <summary>
        /// Delete comment by id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>Deleted comment</returns>
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
