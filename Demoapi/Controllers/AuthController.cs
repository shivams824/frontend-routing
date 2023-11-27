// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Microsoft.Net.Http;
// using Demoapi.Interface;
// using Demoapi.Models;
// using Practice.Dto;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Storage;
// using Demoapi.Authentication;


// namespace Demoapi.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]

//     public class AuthController : Controller
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly RoleManager<User> _roleManager;
//         // private readonly IMapper _mapper;

//         public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
//         {
//             _userManager = userManager;
//             // _roleManager = roleManager;
//         }

//         // public async Task<IActionResult> Register([FromBody] Register register){
//         //     var UserExist = await _userManager.FindByEmailAsync(register.Username);
//         //     if(UserExist != null)
//         //         return StatusCode(StatusCodes.Status500InternalServerError, new Response{Status = "Error", Message = "User Already Exist"});
//         //     ApplicationUser applicationUser = new ApplicationUser() 
//         // }

//         // [HttpPost("login")]
//         // public async Task<ActionResult<UserDto>> Login (LoginDto loginDto)
//         // {
//         //     var user = await _userManager.FindByEmailAsync(loginDto.Email);

//         //     if (user == null) return Unauthorized();

//         //     var results = await _userManager.CheckPasswordAsync(user, loginDto.Password);

//         //     if(results)
//         //     {
//         //         return new UserDto
//         //         {
//         //             DisplayName = user.DisplayName,
//         //             Image = null,
//         //             Token = _tokenservice.CreateToken(user),
//         //             UserName = user.UserName
//         //         };
//         //     }

//         //     return Unauthorized();

//         // }
//     }
// }