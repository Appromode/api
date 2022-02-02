using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(MarkDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Marks.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(MarkDM)))]
        public IActionResult Get(long id)
        {
            var mark = _unitOfWork.Marks.GetById(id);
            if (mark == null)
                return NotFound();
            else
                return Ok(mark);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(MarkDM)))]
        public IActionResult Post([FromBody] MarkDM mark)
        {
            if (mark == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Marks.AddOrUpdate(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(MarkDM)))]
        public IActionResult Put(long id, [FromBody] MarkDM mark)
        {
            if (mark == null)
                return BadRequest();

            if (id != mark.MarkId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Marks.Update(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(MarkDM)))]
        public IActionResult Delete(long id)
        {
            var mark = _unitOfWork.Marks.GetById(id);
            if (mark == null)
                return NotFound();

            mark.deleted = true;
            _unitOfWork.Marks.Update(mark);
            _unitOfWork.Save();

            return Ok(mark);
        }
    }
}
