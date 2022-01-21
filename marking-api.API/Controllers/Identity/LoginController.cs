using marking_api.API.Models.Identity;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace marking_api.API.Controllers.Identity
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private SignInManager<User> _signInManager;

        public LoginController(IUnitOfWork unitOfWork, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(bool)))]
        public bool Logon(string username, string password)
        {
            var cm = new LoginCM(_signInManager, _unitOfWork);
            if (cm.VerifyUser(username, password).Result)
            {
                cm.GenerateLogin(cm.user);
                return true;
            }
            return false;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = (typeof(bool)))]
        public bool Logout()
        {
            var cm = new LoginCM(_signInManager, _unitOfWork);
            cm.Logout();
            return true;
        }

        public bool Verify(string username, string password)
        {
            var cm = new LoginCM(_signInManager, _unitOfWork);
            if (cm.VerifyUser(username, password).Result)
                return true;
            return false;
        }
    }
}
