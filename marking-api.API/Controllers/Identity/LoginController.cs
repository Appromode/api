using marking_api.API.Config;
using marking_api.API.Models.Identity;
using marking_api.Data;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly Jwt _jwt;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public LoginController(IUnitOfWork unitOfWork, SignInManager<User> signInManager, IOptionsMonitor<Jwt> optionsMonitor, TokenValidationParameters tokenValidationParameters)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _jwt = optionsMonitor.CurrentValue;
            _tokenValidationParameters = tokenValidationParameters;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginRequest userLogin)
        {
            if (ModelState.IsValid)
            {
                User user = await _signInManager.UserManager.FindByEmailAsync(userLogin.Email);
                if (user == null)
                    return BadRequest("Invalid Authentication Request");
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, userLogin.Password, false, false);
                    if (!result.Succeeded)
                        return BadRequest("Invalid Authentication Request");
                    if (user.IsDisabled)
                        return BadRequest($"Account for '{userLogin.Email}' is disabled");
                    if (result.IsLockedOut)
                        return BadRequest($"Account for '{userLogin.Email}' is locked out");

                    var cm = new LoginCM(_unitOfWork, _signInManager, _jwt, _tokenValidationParameters);

                    return Ok(cm.GenerateJwtToken(user));
                }            
            }
            return BadRequest("Invalid payload");
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if (ModelState.IsValid)
            {
                var cm = new LoginCM(_unitOfWork, _signInManager, _jwt, _tokenValidationParameters);
                var result = await cm.VerifyAndGenerateToken(tokenRequest);

                if (result == null)
                    return BadRequest("Invalid Tokens");

                return Ok(result);
            }

            return BadRequest("Invalid payload");
        }
    }
}
