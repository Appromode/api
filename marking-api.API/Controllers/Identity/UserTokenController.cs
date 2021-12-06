using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTokenController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserTokenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserTokens.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        public IActionResult Get(string id)
        {
            var userToken = _unitOfWork.UserTokens.GetById(id);
            if (userToken == null)
                return NotFound();
            else
                return Ok(userToken);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        public IActionResult Add([FromBody] UserToken userToken)
        {
            if (userToken == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserTokens.Add(userToken);
            _unitOfWork.Save();

            return Ok(userToken);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        public IActionResult Update(string name, [FromBody] UserToken userToken)
        {
            if (userToken == null)
                return BadRequest();

            if (name != userToken.Name)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserTokens.Update(userToken);
            _unitOfWork.Save();

            return Ok(userToken);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        public IActionResult Delete(string id)
        {
            var userToken = _unitOfWork.UserTokens.GetById(id);
            if (userToken == null)
                return NotFound();

            _unitOfWork.UserTokens.Delete(userToken);
            _unitOfWork.Save();

            return Ok(userToken);
        }
    }
}
