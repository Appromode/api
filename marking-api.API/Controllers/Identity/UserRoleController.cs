using log4net;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserRoleController(IUnitOfWork unitOfWork, ILog logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserRole)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserRoles.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserRole)))]
        public IActionResult Get(string id)
        {
            var userRole = _unitOfWork.UserRoles.GetById(id);
            if (userRole == null)
                return NotFound();
            else
                return Ok(userRole);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserRole)))]
        public IActionResult Post([FromBody] UserRole userRole)
        {
            if (userRole == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserRoles.AddOrUpdate(userRole);
            _unitOfWork.Save();

            return Ok(userRole);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserRole)))]
        public IActionResult Put(string userId, [FromBody] UserRole userRole)
        {
            if (userRole == null)
                return BadRequest();

            if (userId != userRole.UserId)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserRoles.Update(userRole);
            _unitOfWork.Save();

            return Ok(userRole);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserRole)))]
        public IActionResult Delete(string id)
        {
            var userRole = _unitOfWork.UserRoles.GetById(id);
            if (userRole == null)
                return NotFound();

            _unitOfWork.UserRoles.Delete(userRole);
            _unitOfWork.Save();

            return Ok(userRole);
        }
    }
}