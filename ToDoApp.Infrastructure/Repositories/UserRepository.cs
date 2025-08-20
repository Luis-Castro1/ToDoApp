using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;

        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUserByAsync(short type, string user)
        {
            return type switch
            {
                1 => await _userManager.FindByIdAsync(user),
                2 => await _userManager.FindByEmailAsync(user),
                3 => await _userManager.FindByNameAsync(user),
                _ or 0 => null
            };
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }
    }
}
