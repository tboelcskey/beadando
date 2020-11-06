﻿using Data.Models;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class IdentityAuthService : IAuthService
    {
        private readonly UserManager<ImageHubUser> _userManager;
        private readonly SignInManager<ImageHubUser> _signInManager;

        public IdentityAuthService(UserManager<ImageHubUser> userManager, SignInManager<ImageHubUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResult<int>> AttemptLoginAsync(LoginDto login)
        {
            var userInDb = await _userManager.FindByNameAsync(login.Username);
            if (userInDb == null)
            {
                return new AuthResult<int>()
                {
                    Successful = false
                };
            }

            var identitySigninResult = await _signInManager
                .PasswordSignInAsync(login.Username, login.Password, false, false);

            return new AuthResult<int>()
            {
                Successful = identitySigninResult.Succeeded,
                UserId = userInDb.Id
               
            };

        }

        public async Task<AuthResult<int>> AttemptLogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return new AuthResult<int>()
            {
                Successful = true
            };
        }

        public async Task<AuthResult<int>> AttemptRegisterAsync(LoginDto register)
        {
            var user = await _userManager.FindByNameAsync(register.Username);
            if (user != null)
            {
                throw new ApplicationException("User alread exists!");
            }


            // a more approriate way would be to configure a mapping in Automapper
            user = new ImageHubUser();
            user.UserName = register.Username;

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return new AuthResult<int>()
                {
                    Successful = false,
                    Errors = result.Errors.Select(err => err.Description)
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, register.Password, false, false);

            // we add the id of the user role only so that no meaningful info is sent out to the client
            return new AuthResult<int>()
            {
                Successful = true,
                UserId = user.Id
            };
        }
    }
}
