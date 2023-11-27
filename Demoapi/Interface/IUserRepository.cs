using Demoapi.Models;
using Practice.Dto;

namespace Demoapi.Interface
{
    public interface IUserRepository
    {
        public Task<List<UserResponseDto>> GetUsers();
        public Task<User> GetUser(string UserId);
        public Task<User> GetUserByName(string UserName);
        public Task<User> UserExists(string UserId);
        public Task<bool> CreateUser(CreateUserDtoModel requestBody);
        Task<bool> DeleteUser(User user);
        // bool UpdateUserRole(string UserId, User user);
        bool Save();
        public Task<User> GetUserByEmail(string email);
        // Task<bool> DeleteUser(string UserId);
    }
}