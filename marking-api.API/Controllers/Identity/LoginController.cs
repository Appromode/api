using log4net.Core;
using marking_api.API.Models.Identity;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly Jwt _jwt;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public LoginController(IUnitOfWork unitOfWork, SignInManager<User> signInManager, IOptionsMonitor<Jwt> optionsMonitor, TokenValidationParameters tokenValidationParameters, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _jwt = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest userLogin)
        {
            if (ModelState.IsValid)
            {
                User user = _signInManager.UserManager.FindByEmailAsync(userLogin.Email).Result;
                if (user == null)
                    return BadRequest("Invalid Authentication Request");
                else
                {
                    var result = _signInManager.PasswordSignInAsync(user.UserName, userLogin.Password, false, false).Result;
                    if (!result.Succeeded)
                        return BadRequest("Invalid Authentication Request");
                    if (user.IsDisabled)
                        return BadRequest($"Account for '{userLogin.Email}' is disabled");
                    if (result.IsLockedOut)
                        return BadRequest($"Account for '{userLogin.Email}' is locked out");

                    var cm = new LoginCM(_unitOfWork, _signInManager, _jwt, _tokenValidationParameters, _logger);

                    return Ok(cm.GenerateJwtToken(user));
                }
            }
            return BadRequest("Invalid payload");
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var cm = new LoginCM(_unitOfWork, _signInManager, _jwt, _tokenValidationParameters, _logger);
                var result = cm.VerifyAndGenerateToken(tokenRequest).Result;

                if (result == null)
                    return BadRequest("Invalid Tokens");

                return Ok(result);
            }

            return BadRequest("Invalid payload");
        }
    }
}
