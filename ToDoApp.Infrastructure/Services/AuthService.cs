using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ForgotPasswordResponseDto> GenerateForgotPasswordCode(string UserOEmail)
        {
            var user = await FindUser(UserOEmail);

            if (user == null)
            {
                return new ForgotPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "User not found."
                };
            }

            var code = GenerateSecureCode();
            var expiresAt = DateTime.UtcNow.AddMinutes(10);
            var tokenValue = $"{code}|{expiresAt}";


            await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode", tokenValue);
            await _emailService.SendEmailAsync(user.Email!, "Password Reset Code", $"Your password reset code is: {code}");

            return new ForgotPasswordResponseDto { IsSuccess = true };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await FindUser(loginDto.User);

            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsAuthenticated = false,
                    ErrorMessage = "User not found."
                };
            }

            var result = _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, false);

            if (!result.Result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Login failed. Please check your credentials."
                };
            }

            return new AuthResponseDto
            {
                IsAuthenticated = true,
                UserName = user.UserName,
                ErrorMessage = string.Empty
            };
        }

        private async Task<User> FindUser(string userOrEmail)
        {
            var user = await _userManager.FindByNameAsync(userOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(userOrEmail);

                if (user == null)
                {
                    return null;
                }
            }

            return user;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var user = await FindUser(resetPasswordDto.UserOrEmail);
            if (user == null)
            {
                return new ResetPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "User not found."
                };
            }

            var savedValue = await _userManager.GetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");
            if (string.IsNullOrEmpty(savedValue))
            {
                return new ResetPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "No reset code found. Please request a new one."
                };
            }

            var parts = savedValue.Split('|');
            var savedCode = parts[0];
            var expiresAt = DateTime.Parse(parts[1], null, System.Globalization.DateTimeStyles.RoundtripKind);

            if (DateTime.UtcNow > expiresAt)
            {
                return new ResetPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "The reset code has expired. Please request a new one."
                };
            }

            // Invalida el token para que no se pueda reutilizar
            await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");

            // Reset de la contraseña
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new ResetPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }

            return new ResetPasswordResponseDto
            {
                IsSuccess = true
            };

        }

        private string GenerateSecureCode()
        {
            var bytes = new byte[4];
            RandomNumberGenerator.Fill(bytes);
            int value = BitConverter.ToInt32(bytes, 0);
            value = Math.Abs(value % (int)Math.Pow(10, 6));
            return value.ToString(new string('0', 6));

        }
    }
}
