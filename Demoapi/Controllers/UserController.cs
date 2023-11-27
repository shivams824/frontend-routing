using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Demoapi.Interface;
using Demoapi.Models;
using Practice.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Demoapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<ActionResult<List<UserResponseDto>>> GetUsers()
        {
            try
            {
                var userList = await _userRepository.GetUsers();
                if (userList.Any())
                {
                    return Ok(userList);
                }
                else
                {
                    return NotFound("No Users Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }



        // [Authorize(Roles ="Admin")]
        [AllowAnonymous]
        [HttpGet("{UserId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _userRepository.UserExists(UserId);
                if (exist != null)
                {
                    var user = await _userRepository.GetUser(UserId);
                    return Ok(user);
                }
                else
                {
                    return NotFound("No Users Found");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        [HttpPost("create")]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDtoModel requestBody)
        {
            //Checking the modelstate.
            if (ModelState.IsValid)
            {
                try
                {
                    var Exists = await _userRepository.GetUserByEmail(requestBody.Email);
                    if (Exists == null)
                    {
                        var result = await _userRepository.CreateUser(requestBody);
                        if (result)
                        {
                            return BadRequest("User creation has failed");
                        }
                        else
                        {
                            return Ok("User has been successfully created");
                        }

                    }
                    else
                    {
                        return Conflict("User with same email already exists.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception", ex.Message);
                    return UnprocessableEntity();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [AllowAnonymous]
        [HttpDelete("{UserId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult> DeleteUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // var Exists = await _userRepository.GetUserByEmail(UserId);
                    var Exists = await _userRepository.GetUser(UserId);
                    if (Exists != null)
                    {
                        bool result = await _userRepository.DeleteUser(Exists);
                        if (result)
                        {
                            return BadRequest("User deletion has failed");
                        }
                        else
                        {
                            return Ok("User has been successfully deleted");
                        }
                    }
                    else
                    {
                        return Conflict("User does not exists.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception", ex.Message);
                    return UnprocessableEntity();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //check that the given user id is not null or empty.
        //check if the user with this id exists or not.
        //delete the user. returns bool
        //if true return ok("User deleted sucess")
        //user deletion failed.


        // [HttpPut("updaterole")]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(404)]

        // public async IActionResult UpdateUserRole(string UserId, [FromQuery] UserDto requestBody)
        // {

        //     if (ModelState.IsValid)
        //     {
        //         if (updateuser != null)
        //         {
        //             var user = await _userRepository.GetUserByEmail(requestBody.Email);
        //             if(user != null)
        //             {
        //                 _userRepository.UpdateUserRole(UserId, userMap)

        //             return NoContent();
        //             }


        //         }
        //         else
        //         {
        //             return BadRequest();
        //         }


        //     }
        //     else
        //     {
        //         return BadRequest(ModelState);
        //     }
        // }

    }
}