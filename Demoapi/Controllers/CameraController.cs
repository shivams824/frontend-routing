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

    public class CameraController : Controller
    {
        private readonly ICameraRepository _cameraRepository;
        private readonly IUserRepository _userRepository;


        public CameraController(ICameraRepository cameraRepository, IUserRepository userRepository)
        {
            _cameraRepository = cameraRepository;
            _userRepository = userRepository;
        }

        //GET ALL CAMERA//
        [Authorize(Roles = "Admin")]
        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Camera>))]

        public async Task<ActionResult<List<CameraResponseDto>>> GetCameras()
        {
            try
            {
                var cameraslist = await _cameraRepository.GetCameras();
                if (cameraslist.Any())
                {
                    return Ok(cameraslist);
                }
                else
                {
                    return NotFound("No cameras Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }


        //GET CAMERA BY ID//
        [AllowAnonymous]
        [HttpGet("{CameraId}")]
        [ProducesResponseType(200, Type = typeof(Camera))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetCamera(Guid CameraId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _cameraRepository.CameraExists(CameraId);
                if (exist != null)
                {
                    var camera = await _cameraRepository.GetCamera(CameraId);
                    return Ok(camera);
                }
                else
                {
                    return NotFound("No Cameras Found");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //GET PUMPS BY USER
            [AllowAnonymous]
            [HttpGet("{UserId}/cameras")]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Camera>))]
            [ProducesResponseType(400)]

            public async Task<ActionResult> GetCamerasByUser(string UserId)
            {
                if (ModelState.IsValid)
                {
                    var exist = await _userRepository.UserExists(UserId);
                    if (exist != null)
                    {
                        var camera = await _cameraRepository.GetCamerasByUser(UserId);
                        return Ok(camera);
                    }
                    else
                    {
                        return NotFound("No cameras exist for specified User");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }


        //CREATE CAMERA//
        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult> CreateCamera([FromBody] CreateCameraDtoModel requestBody)
        {
            //Checking the modelstate.
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _cameraRepository.CreateCamera(requestBody);
                    if (result)
                    {
                        return Ok("Camera has been successfully created");
                    }
                    else
                    {
                        return BadRequest("Camera creation has failed");
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

        //UPDATE CAMERA//
        [AllowAnonymous]
        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateCamera(Guid CameraId, [FromBody] CameraUpdateDto updatecamera)
        {
            if (ModelState.IsValid)
            {
                var exist = await _cameraRepository.CameraExists(CameraId);
                if (exist == null)
                {
                    return NotFound("Camera not found");
                }

                bool result = _cameraRepository.UpdateCamera(CameraId, updatecamera);

                if (result)
                {
                    return Ok("Camera Status updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update the Camera");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        //DELETE CAMERA//  
        [AllowAnonymous]
        [HttpDelete("{CameraId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> DeleteCamera(Guid CameraId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _cameraRepository.DeleteCamera(CameraId);
                    if (result)
                    {
                        return Ok("Camera has been successfully deleted");
                    }
                    else
                    {
                        return BadRequest("Camera deletion has failed");
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

        //CREATE CAMERA DESCRIPTION
        // [HttpPost]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // public IActionResult CreateCamDesc (/*[FromQuery] Guid CameraId,*/ [FromQuery] CameraDto cameradescription)
        // {
        //     if(cameradescription == null)
        //         return BadRequest(ModelState);

        //     var desc = _cameraRepository.GetCameras()
        //         .Where(c => c.Title.Trim().ToUpper() == pumpdescription.Title.TrimEnd().ToUpper())
        //         .FirstOrDefault();

        //     if (pumpdescription != null)
        //     {
        //         ModelState.AddModelError("","Description already exists");
        //         return StatusCode(422, ModelState);
        //     }

        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var pumpMap = _mapper.Map<Pump>(pumpdescription);

        //     pumpMap.PumpName = _pumpRepository.CreatePumpDesc(PumpId);
        // }

        //UPDATE CAMERA DESCRIPTION

        //DELETE CAMERA DESCRIPTION
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
    }
}