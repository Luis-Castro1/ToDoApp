using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task LogoutAsync();

    }
}
