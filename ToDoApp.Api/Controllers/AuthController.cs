using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Invalid login request.");
            }

            if (string.IsNullOrEmpty(loginDto.User) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("User and password are required.");
            }

            var authResponse = await _authService.LoginAsync(loginDto);
            if (!authResponse.IsAuthenticated)
            {
                return Unauthorized(authResponse.ErrorMessage);
            }

            return Ok(authResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { Message = "Logged out successfully." });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string userOrEmail)
        {
            if (string.IsNullOrEmpty(userOrEmail))
            {
                return BadRequest("User or email is required.");
            }
            var forgotPasswordResponse = await _authService.GenerateForgotPasswordCode(userOrEmail);
            if (!forgotPasswordResponse.IsSuccess)
            {
                return NotFound(forgotPasswordResponse.ErrorMessage);
            }
            return Ok(new {message = "Se ha enviado un código de recuperación al correo asociado." });
        }

        [HttpPost("validate-code-forgot-password")]
        public async Task<IActionResult> ValidateCodeForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (forgotPasswordDto == null)
            {
                return BadRequest("Invalid validate code request.");
            }

            if (string.IsNullOrWhiteSpace(forgotPasswordDto.User) || string.IsNullOrWhiteSpace(forgotPasswordDto.Code))
            {
                return BadRequest("User and Code are required.");
            }

            var result = await _authService.ValidateForgotPasswordCode(forgotPasswordDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new { errorMessage = result.ErrorMessage });
            }
            return Ok(new { Message = "Code is valid." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null)
            {
                return BadRequest("Invalid reset password request.");
            }
            if (string.IsNullOrEmpty(resetPasswordDto.UserOrEmail) || string.IsNullOrEmpty(resetPasswordDto.Token) || string.IsNullOrEmpty(resetPasswordDto.NewPassword))
            {
                return BadRequest("User or email, code, and new password are required.");
            }

            var result = await _authService.ResetPasswordAsync(resetPasswordDto);

            if (!result.IsSuccess)
            {
                return BadRequest(new {errorMessage = result.ErrorMessage });
            }

            return Ok(new { Message = "Password has been reset successfully." });
        }
    }
}
