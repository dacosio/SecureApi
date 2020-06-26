using Microsoft.AspNetCore.Identity;
using SecureAPI.DbContext;
using SecureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Services
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<UserManagerResponseDto> RegisterUserAsync(RegisterViewModelDto model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponseDto
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponseDto
                {
                    Message = "User created successfully",
                    IsSuccess = true
                };
            }

            return new UserManagerResponseDto
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }   
    }
}
