using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Demoapi.Dto;
using Demoapi.Interface;
using Demoapi.Models;
using Practice.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Demoapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class WheelController : Controller
    {
        private readonly IWheelRepository _wheelRepository;
        private readonly IUserRepository _userRepository;

        public WheelController(IWheelRepository wheelRepository, IUserRepository userRepository)
        {
            _wheelRepository = wheelRepository;
            _userRepository = userRepository;
        }

        //GET ALL WHEELS
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wheel>))]

        public async Task<ActionResult<List<WheelResponseDto>>> GetWheels()
        {
            try
            {
                var wheelslist = await _wheelRepository.GetWheels();
                if (wheelslist.Any())
                {
                    return Ok(wheelslist);
                }
                else
                {
                    return NotFound("No Wheels Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }


        //GET WHEEL BY ID
        [AllowAnonymous]
        [HttpGet("{WheelId}")]
        [ProducesResponseType(200, Type = typeof(Wheel))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetWheel(Guid WheelId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _wheelRepository.WheelExists(WheelId);
                if (exist != null)
                {
                    var wheel = await _wheelRepository.GetWheel(WheelId);
                    return Ok(wheel);
                }
                else
                {
                    return NotFound("No Wheels Found");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //GET WHEELS BY USER
        [AllowAnonymous]
        [HttpGet("{UserId}/wheels")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Wheel>))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetWheelsByUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _userRepository.UserExists(UserId);
                if (exist!=null)
                {
                    var wheel = await _wheelRepository.GetWheelsByUser(UserId);
                    return Ok(wheel);
                }
                else
                {
                    return NotFound("No Wheels exist for specified User");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //CREATE WHEEL
        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult> CreateWheel([FromBody] CreateWheelDtoModel requestBody)
        {
            //Checking the modelstate.
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _wheelRepository.CreateWheel(requestBody);
                    if (result)
                    {
                        return Ok("Wheel has been successfully created");
                    }
                    else
                    {
                        return BadRequest("Wheel creation has failed");
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

        //UPDATE WHEEL
        [AllowAnonymous]
        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateWheel(Guid WheelId, [FromBody] WheelUpdateDto updatewheel)
        {
            if (ModelState.IsValid)
            {
                var exist = await _wheelRepository.WheelExists(WheelId);
                if (exist == null)
                {
                    return NotFound("Wheel not found");
                }

                bool result = _wheelRepository.UpdateWheel(WheelId, updatewheel);

                if (result)
                {
                    return Ok("Wheel Status updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update the Wheel");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE WHEEL
        [AllowAnonymous]
        [HttpDelete("{WheelId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteWheel(Guid WheelId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _wheelRepository.DeleteWheel(WheelId);
                    if (result)
                    {
                        return Ok("Wheel has been successfully deleted");
                    }
                    else
                    {
                        return BadRequest("Wheel deletion has failed");
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


        //CREATE WHEEL DESCRIPTION
        // [HttpPost]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // public IActionResult WheelDescription([FromQuery] Guid WheelId, [FromQuery] WheelDto wheeldescription)
        // {
        //     if(wheeldescription == null)
        //         return BadRequest(ModelState);

        //     var desc = _wheelRepository.GetWheels()
        //         .Where(c => c.Title.Trim().ToUpper() == wheeldescription.Title.TrimEnd().ToUpper())
        //         .FirstOrDefault();

        //     if (wheeldescription != null)
        //     {
        //         ModelState.AddModelError("","Description already exists");
        //         return StatusCode(422, ModelState);
        //     }

        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var wheelMap = _mapper.Map<Wheel>()
        // }


        //DELETE WHEEL DESCRIPTION
        // [HttpDelete("{CameraId}")]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(404)]

        // public IActionResult DeleteCamDesc(Guid CameraId)
        // {
        //     if(!_cameraRepository.CameraDesc(CameraId))
        //         return NotFound();

        //     var cameradescdelete = _cameraRepository.GetCamera(CameraId);

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     if(!_cameraRepository.DeleteCamDesc(cameradescdelete))
        //     {
        //         ModelState.AddModelError("", "Something went wrong while deleting the Description");

        //     }

        //     return NoContent();
        // }


        // //CREATE WHEEL
        //         [HttpPost]
        //         [ProducesResponseType(204)]
        //         [ProducesResponseType(400)]

        //         public IActionResult CreateWheel([FromQuery] Guid WheelId, /*[FromQuery] int categoryId,*/ [FromBody] WheelDto wheelCreate)
        //         {
        //             if(wheelCreate == null)
        //                 return BadRequest(ModelState);

        //             var wheel = _wheelRepository.GetWheels().
        //             Where(c => c.WheelName.Trim().ToUpper() == wheelCreate.WheelName.TrimEnd().ToUpper()).FirstOrDefault();
        //             //Where(c => c.LastName.Trim().ToUpper() == WheelCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();


        //             if(wheel != null)
        //             {
        //                 ModelState.AddModelError("", "Wheel Already Exists");
        //                 return StatusCode(422, ModelState);
        //             }

        //             if(!ModelState.IsValid)
        //                 return BadRequest(ModelState);

        //             var wheelMap = _mapper.Map<Wheel>(wheelCreate);

        //             if(!_wheelRepository.CreateWheel(WheelId, /*categoryId,*/ wheelMap))
        //             {
        //                 ModelState.AddModelError("","Something went wrong while saving");
        //                 return StatusCode(500, ModelState);
        //             }

        //             return Ok("Successfully Created");

        //         }
    }
}