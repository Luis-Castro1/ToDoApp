using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
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
            using var sha = SHA256.Create();
            var codeHash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(code)));

            var data = new
            {
                CodeHash = codeHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                Attempts = 0
            };

            var tokenValue = JsonSerializer.Serialize(data);

            await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode", tokenValue);
            await _emailService.SendEmailAsync(user.Email!, "Password Reset Code", $"Your password reset code is: {code}");

            return new ForgotPasswordResponseDto { IsSuccess = true };
        }
        public async Task<ValidateCodeResponseDto> ValidateForgotPasswordCode(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await FindUser(forgotPasswordDto.User);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var savedJson = await _userManager.GetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");

            if (string.IsNullOrEmpty(savedJson))
            {
                throw new Exception("No reset code found. Please request a new one.");
            }

            var data = JsonSerializer.Deserialize<Dictionary<string, object>>(savedJson)!;
            var expiresAt = DateTime.Parse(data["ExpiresAt"].ToString()!);
            var attempts = Convert.ToInt32(data["Attempts"].ToString());
            var savedHash = data["CodeHash"].ToString()!;

            if (DateTime.UtcNow > expiresAt) throw new Exception("Code expired.");
            if (attempts >= 5)
            {
                throw new Exception("Too many attempts.");
            }

            // Comparar hash
            using var sha = SHA256.Create();
            var inputHash = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(forgotPasswordDto.Code)));

            if (inputHash != savedHash)
            {
                // Incrementar intentos
                data["Attempts"] = attempts + 1;
                await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "ResetCode", JsonSerializer.Serialize(data));
                throw new Exception("Invalid code.");
            }

            // Invalida el token para que no se pueda reutilizar
            await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "ResetCode");

            // ✅ Generar RecoverySessionId (GUID único)
            var recoverySessionId = Guid.NewGuid().ToString("N");
            var sessionExpiresAt = DateTime.UtcNow.AddMinutes(5);
            await _userManager.SetAuthenticationTokenAsync(user, "PasswordReset", "RecoverySession", $"{recoverySessionId}|{sessionExpiresAt:o}");
            return new ValidateCodeResponseDto { IsSuccess = true , RecoverySessionId = recoverySessionId};

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


            var sessionValue = await _userManager.GetAuthenticationTokenAsync(user, "PasswordReset", "RecoverySession");
            if (string.IsNullOrEmpty(sessionValue))
                return new ResetPasswordResponseDto { IsSuccess = false, ErrorMessage = "Invalid session." };

            var parts = sessionValue.Split('|');
            var savedSessionId = parts[0];
            var expiresAt = DateTime.Parse(parts[1]);

            if (DateTime.UtcNow > expiresAt)
                return new ResetPasswordResponseDto { IsSuccess = false, ErrorMessage = "Session expired." };

            if (savedSessionId != resetPasswordDto.Token)
                return new ResetPasswordResponseDto { IsSuccess = false, ErrorMessage = "Invalid session id." };


            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Reset de la contraseña
            var result = await _userManager.ResetPasswordAsync(user, resetToken, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new ResetPasswordResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = string.Join("; ", result.Errors.Select(e => e.Description))
                };
            }

            // 🔒 Eliminar el session token para que no se reutilice
            await _userManager.RemoveAuthenticationTokenAsync(user, "PasswordReset", "RecoverySession");

            return new ResetPasswordResponseDto { IsSuccess = true };

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
