using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using marking_api.API.Config;

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
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]

        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserTokens.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
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
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] UserToken userToken)
        {
            if (userToken == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserTokens.AddOrUpdate(userToken);
            _unitOfWork.Save();

            return Ok(userToken);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserToken)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Put(string name, [FromBody] UserToken userToken)
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
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
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
