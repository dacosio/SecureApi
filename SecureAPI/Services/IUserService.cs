using SecureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.Services
{
    public interface IUserService
    {
        Task<UserManagerResponseDto> RegisterUserAsync(RegisterViewModelDto model);
        Task<UserManagerResponseDto> LogInUserAsync(LoginViewModelDto model);

    }
}
