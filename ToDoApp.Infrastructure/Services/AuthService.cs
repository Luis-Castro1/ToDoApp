using Microsoft.AspNetCore.Identity;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private AuthResponseDto _authResponse = new();
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.User);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.User);

                if (user == null)
                {

                    _authResponse.IsAuthenticated = false;
                    _authResponse.ErrorMessage = "User not found.";
                    return _authResponse;
                }

            }

            var result = _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);

            if (!result.Result.Succeeded)
            {
                _authResponse.IsAuthenticated = false;
                _authResponse.ErrorMessage = "Credenciales Inválidas.";
                return _authResponse;
            }

            _authResponse.IsAuthenticated = true;
            _authResponse.UserName = user.UserName;
            _authResponse.ErrorMessage = string.Empty;
            return _authResponse;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
