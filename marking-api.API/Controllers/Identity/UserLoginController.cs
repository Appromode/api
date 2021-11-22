using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserLogins.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        public IActionResult Get(string id)
        {
            var userLogin = _unitOfWork.UserLogins.GetById(id);
            if (userLogin == null)
                return NotFound();
            else
                return Ok(userLogin);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        public IActionResult Post([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserLogins.Update(userLogin);
            _unitOfWork.Save();

            return Ok(userLogin);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        public IActionResult Put(string providerKey, [FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest();

            if (providerKey != userLogin.ProviderKey)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserLogins.Update(userLogin);
            _unitOfWork.Save();

            return Ok(userLogin);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        public IActionResult Delete(string id)
        {
            var userLogin = _unitOfWork.UserLogins.GetById(id);
            if (userLogin == null)
                return NotFound();

            _unitOfWork.UserLogins.Delete(userLogin);
            _unitOfWork.Save();

            return Ok(userLogin);
        }
    }
}
