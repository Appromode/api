using log4net;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// Grade API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public GradeController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get grades
        /// </summary>
        /// <returns>List of grades</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(GradeDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Grades.Get());
        }

        /// <summary>
        /// Get grade by id
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>List of grades</returns>
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

        /// <summary>
        /// Post grade
        /// </summary>
        /// <param name="grade">GradeDM</param>
        /// <returns>Saved GradeDM</returns>
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

        /// <summary>
        /// Put grade by id
        /// </summary>
        /// <param name="id">long</param>
        /// <param name="grade">GradeDM</param>
        /// <returns>Saved GradeDM</returns>
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

        /// <summary>
        /// Delete grade
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>Deleted GradeDM</returns>
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