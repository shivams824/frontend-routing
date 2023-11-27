using Microsoft.AspNetCore.Mvc;
using Demoapi.Models;
using Practice.Dto;
using Microsoft.AspNetCore.Identity;
using Demoapi.Authentication;
using API.Services;
using Microsoft.AspNetCore.Authorization;


namespace Demoapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;


        public LoginController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, TokenService tokenService, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);

            if (user == null)
            {
                return Conflict("User with given Email does not exist");
            }
            else
            {
                var results = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                var role = await _userManager.GetRolesAsync(user);

                if (results)
                {
                    return new UserDto
                    {
                        Role = user.Role,
                        FirstName = user.FirstName,
                        Token = _tokenService.CreateToken(user, role, _configuration),
                        Email = user.Email,
                        EmailConfirmed = true
                    };
                }
            }
            return Unauthorized();
        }
    }
}