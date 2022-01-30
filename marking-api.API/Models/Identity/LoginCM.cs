using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace marking_api.API.Models.Identity
{
    public class LoginCM : BaseModel
    {
        public IUnitOfWork _unitOfWork;
        public SignInManager<User> _signInManager;
        public LoginCM(SignInManager<User> signInManager, IUnitOfWork unitOfwork)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfwork;
        }

        public User user { get; set; }

        public bool Login(string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                User inituser = _signInManager.UserManager.FindByEmailAsync(email).Result; //_signInManager.PasswordSignInAsync(email, password, true, false);// .PasswordSignInAsync(username, password, true, false).Result;
                if (inituser == null)
                    return false;
                var result = _signInManager.PasswordSignInAsync(inituser.UserName, password, true, false).Result;
                
                if (result.Succeeded)
                {
                    if (user.IsDisabled)
                    {
                        return false;
                    }
                    this.user = inituser;
                    return true;
                }
                if (result.RequiresTwoFactor)
                {
                    return false;
                }
                if (result.IsLockedOut)
                {
                    return false;
                }
                if (result.IsNotAllowed)
                {
                    return false;
                }
            }
            return false;
        }

        public bool VerifyUser(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                User user = _signInManager.UserManager.FindByNameAsync(username).Result;
                if (user != null)
                {
                    var result = _signInManager.UserManager.CheckPasswordAsync(user, password).Result;
                    if (result)
                    {
                        return true;
                    }                    
                }                
            }
            return false;
        }

        //public void GenerateLogin(User user)
        //{
        //    _unitOfWork.UserLogins.Add(new UserLogin 
        //    {
        //        UserId = user.Id,
        //        LoginProvider = "frontend",
        //        ProviderDisplayName = "FrontEnd",
        //        User = user,
        //        ProviderKey = Guid.NewGuid().ToString()
        //    });
        //    _unitOfWork.Save();
        //}

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
