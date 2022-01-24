﻿using marking_api.DataModel.Identity;
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

        public async Task<bool> VerifyUser(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, true, false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    var user = await _signInManager.UserManager.FindByNameAsync(username);
                    if (user.IsDisabled)
                    {
                        return false;
                    }
                    this.user = user;
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
            }
            return false;
        }

        public void GenerateLogin(User user)
        {
            _unitOfWork.UserLogins.Add(new UserLogin 
            {
                UserId = user.Id,
                LoginProvider = "frontend",
                ProviderDisplayName = "FrontEnd",
                User = user,
                ProviderKey = Guid.NewGuid().ToString()
            });
            _unitOfWork.Save();
        }

        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}