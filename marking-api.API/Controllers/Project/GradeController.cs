using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Grades.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Get(long id)
        {
            var grade = _unitOfWork.Grades.GetById(id);
            if (grade == null)
                return NotFound();
            else
                return Ok(grade);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Post([FromBody] GradeDM grade)
        {
            if (grade == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Grades.AddOrUpdate(grade);
            _unitOfWork.Save();

            return Ok(grade);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Put(long id, [FromBody] GradeDM grade)
        {
            if (grade == null)
                return BadRequest();

            if (id != grade.GradeId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Save();

            return Ok(grade);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Delete(long id)
        {
            var grade = _unitOfWork.Grades.GetById(id);
            if (grade == null)
                return NotFound();

            grade.deleted = true;
            _unitOfWork.Grades.Update(grade);
            _unitOfWork.Save();

            return Ok(grade);
        }
    }
}