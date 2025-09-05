using ToDoApp.Mobile.Core.DTOs;

namespace ToDoApp.Mobile.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(string user, string password);
        Task<GeneralResponseDto> GenerateForgotPasswordCode(string userOrEmail);
        Task<GeneralResponseDto> ValidateForgotPasswordCode(string userOrEmail, string code);
        Task<GeneralResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task LogoutAsync();
    }
}
