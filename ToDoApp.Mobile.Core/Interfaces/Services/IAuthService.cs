using ToDoApp.Mobile.Core.Dtos;

namespace ToDoApp.Mobile.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(string email, string password);
        Task LogoutAsync();
    }
}
