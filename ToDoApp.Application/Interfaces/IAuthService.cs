using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
        Task<ForgotPasswordResponseDto> GenerateForgotPasswordCode(string UserOEmail);
        Task<ResetPasswordResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<ValidateCodeResponseDto> ValidateForgotPasswordCode(ForgotPasswordDto forgotPasswordDto);
    }
}
