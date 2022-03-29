using log4net.Core;
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
    public class UserLoginController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserLoginController(IUnitOfWork unitOfWork, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.UserLogins.Get());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
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
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
        public IActionResult Post([FromBody] UserLogin userLogin)
        {
            if (userLogin == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _unitOfWork.UserLogins.AddOrUpdate(userLogin);
            _unitOfWork.Save();

            return Ok(userLogin);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(UserLogin)))]
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
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
        [ClaimRequirement(MarkingClaimTypes.Permission, "Identity")]
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
