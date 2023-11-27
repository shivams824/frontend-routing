using Demoapi.Data;
using Demoapi.Interface;
using Demoapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Practice.Dto;

namespace Demoapi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateUser(CreateUserDtoModel requestBody)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(requestBody.Email);

                if (user == null)
                {
                    var newUser = new User
                    {
                        UserName = requestBody.UserName,
                        Email = requestBody.Email,
                        Role = requestBody.Role,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedOn = DateTime.UtcNow,
                    };

                    var result = await _userManager.CreateAsync(newUser, requestBody.Password);

                    if (result.Succeeded)
                    {
                        var addedUser = await _userManager.FindByEmailAsync(requestBody.Email);
                        await _userManager.AddToRoleAsync(addedUser, requestBody.Role);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex.Message);
                return false;
            }
        }



        public async Task<bool> DeleteUser(User user)
        {
            await _userManager.DeleteAsync(user);
            return Save();
        }
        public async Task<User> GetUser(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        public async Task<User> GetUserByName(string Name)
        {
            return await _context.Users.Where(p => p.FirstName == Name).FirstOrDefaultAsync();
        }

        public async Task<List<UserResponseDto>> GetUsers()
        {
            return await _context.Users.Select(x => new UserResponseDto
            {
                UserId = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Role = x.Role
            }).ToListAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }


        public async Task<User> UserExists(string Id)
        {
            return await _userManager.FindByIdAsync(Id);
        }

        // public async bool UpdateUserRole(string email, string role)
        // {
        //     var user = await _userManager.FindByEmailAsync(email);
        //     user.Role = new IList<string>();
        //     _userManager.UpdateAsync()

        //     //update role by using usermanager
        //     //remove role from user
        //     //add role to user.
        // }

        // public async Task<bool> DeleteUser(string UserId)
        // {
        //     var user = await _context.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        //     _context.Remove(user);
        //     return Save();
        // }
    }
}
