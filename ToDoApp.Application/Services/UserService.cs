using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Interfaces;

namespace ToDoApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CreateUserAsync(UserPostDto userToCreate, string password)
        {
            var user = _mapper.Map<User>(userToCreate);
            var result = await _userRepository.CreateAsync(user, userToCreate.Password);
            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(UserDto userToDelete)
        {
            var user = _mapper.Map<User>(userToDelete);
            var result = await _userRepository.DeleteAsync(user);
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<UserDto>>(users);
            return result;
        }

        public async Task<UserDto?> GetUserByAsync(short type, string u)
        {
            var user = await _userRepository.GetUserByAsync(type, u);
            var result = _mapper?.Map<UserDto>(user);
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserDto userToUpdate)
        {
            var user = _mapper.Map<User>(userToUpdate);
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
