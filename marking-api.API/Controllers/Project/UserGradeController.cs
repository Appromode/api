using marking_api.DataModel.Project;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marking_api.API.Controllers.Project
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserGradeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserGradeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserGrades.Get());
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserGradeDM)))]
        public IActionResult Post([FromBody] UserGradeDM userGrade)
        {
            if (userGrade == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserGrades.Update(userGrade);
            _unitOfWork.Save();

            return Ok(userGrade);
        }

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