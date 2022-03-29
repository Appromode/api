using log4net.Core;
using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    /// <summary>
    /// UserGrade API Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserGradeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Constructor initialising unitofwork
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public UserGradeController(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET List of UserGradeDMs
        /// </summary>
        /// <returns>List of UserGradeDM</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserGrades.Get());
        }

        /// <summary>
        /// GET UserGradeDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>UserGradeDM</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Get(long id)
        {
            var userGrade = _unitOfWork.UserGrades.GetById(id);
            if (userGrade == null)
                return NotFound();
            else
                return Ok(userGrade);
        }

        /// <summary>
        /// POST UserGradeDM
        /// </summary>
        /// <param name="userGrade">UserGradeDM</param>
        /// <returns>Saved or updated UserGradeDM</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Post([FromBody] UserGradeDM userGrade)
        {
            if (userGrade == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserGrades.AddOrUpdate(userGrade);
            _unitOfWork.Save();

            return Ok(userGrade);
        }

        /// <summary>
        /// PUT UserGradeDM by id
        /// </summary>
        /// <param name="id">Int64</param>
        /// <param name="userGrade">UserGradeDM</param>
        /// <returns>Updated UserGradeDM</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Put(long id, [FromBody] UserGradeDM userGrade)
        {
            if (userGrade == null)
                return BadRequest();

            if (id != userGrade.UserGradeId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserGrades.Update(userGrade);
            _unitOfWork.Save();

            return Ok(userGrade);
        }

        /// <summary>
        /// DELETE UserGradeDM
        /// </summary>
        /// <param name="id">Int64</param>
        /// <returns>Deleted UserGradeDM</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Delete(long id)
        {
            var userGrade = _unitOfWork.UserGrades.GetById(id);
            if (userGrade == null)
                return NotFound();

            userGrade.deleted = true;
            _unitOfWork.UserGrades.Update(userGrade);
            _unitOfWork.Save();

            return Ok(userGrade);
        }
    }
}