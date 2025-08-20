using Microsoft.AspNetCore.Identity;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByAsync(short type, string user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
    }
}
