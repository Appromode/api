using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserClaimController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserClaimController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserClaim)))]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserClaims.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserClaim)))]
        public IActionResult Get(string id)
        {
            var userClaim = _unitOfWork.UserClaims.GetById(id);
            if (userClaim == null)
                return NotFound();
            else
                return Ok(userClaim);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserClaim)))]
        public IActionResult Add([FromBody] UserClaim userClaim)
        {
            if (userClaim == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserClaims.Add(userClaim);
            _unitOfWork.Save();

            return Ok(userClaim);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserClaim)))]
        public IActionResult Update(int id, [FromBody] UserClaim userClaim)
        {
            if (userClaim == null)
                return BadRequest();

            if (id != userClaim.Id)
                return BadRequest("Id Mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserClaims.Update(userClaim);
            _unitOfWork.Save();

            return Ok(userClaim);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserClaim)))]
        public IActionResult Delete(string id)
        {
            var userClaim = _unitOfWork.UserClaims.GetById(id);
            if (userClaim == null)
                return NotFound();

            _unitOfWork.UserClaims.Delete(userClaim);
            _unitOfWork.Save();

            return Ok(userClaim);
        }
    }
}
