using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ToDoApp.Application.DTOs;

namespace ToDoApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByAsync(short type, string user);
        Task<IdentityResult> CreateUserAsync(UserPostDto userToCreate, string password);
        Task<IdentityResult> UpdateUserAsync(UserDto userToUpdate);
        Task<IdentityResult> DeleteUserAsync(UserDto userToDelete);
    }
}
