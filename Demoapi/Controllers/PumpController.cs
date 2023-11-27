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

    public class PumpController : Controller
    {
        private readonly IPumpRepository _pumpRepository;
        private readonly IUserRepository _userRepository;

        public PumpController(IPumpRepository pumpRepository, IUserRepository userRepository)
        {
            _pumpRepository = pumpRepository;
            _userRepository = userRepository;
        }

        //GET ALL PUMPS//
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pump>))]

        public async Task<ActionResult<List<PumpResponseDto>>> GetPumps()
        {
            try
            {
                var pumpslist = await _pumpRepository.GetPumps();
                if (pumpslist.Any())
                {
                    return Ok(pumpslist);
                }
                else
                {
                    return NotFound("No pumps Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }


        //GET PUMP BY ID//
        [AllowAnonymous]
        [HttpGet("{PumpId}")]
        [ProducesResponseType(200, Type = typeof(Pump))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetPump(Guid PumpId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _pumpRepository.PumpExists(PumpId);
                if (exist != null)
                {
                    var pump = await _pumpRepository.GetPump(PumpId);
                    return Ok(pump);
                }
                else
                {
                    return NotFound("No Pumps Found");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        ///////
        //GET PUMPS BY USER
        [AllowAnonymous]
        [HttpGet("{UserId}/pumps")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pump>))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetPumpsByUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _userRepository.UserExists(UserId);
                if (exist != null)
                {
                    var pump = await _pumpRepository.GetPumpsByUser(UserId);
                    return Ok(pump);
                }
                else
                {
                    return NotFound("No Pumps exist for specified User");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        ///
        [AllowAnonymous]
        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public async Task<ActionResult> CreatePump([FromBody] CreatePumpDtoModel requestBody)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _pumpRepository.CreatePump(requestBody);
                    if (result)
                    {
                        return Ok("Pump has been successfully created");
                    }
                    else
                    {
                        return BadRequest("Pump creation has failed");
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


        //UPDATE PUMP//
        [AllowAnonymous]
        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdatePump(Guid PumpId, [FromForm] PumpUpdateDto updatepump)
        {
            if (ModelState.IsValid)
            {
                var exist = await _pumpRepository.PumpExists(PumpId);
                if (exist == null)
                {
                    return NotFound("Pump not found");
                }

                bool result = _pumpRepository.UpdatePump(PumpId, updatepump);

                if (result)
                {
                    return Ok("Pump Status updated successfully");
                }
                else
                {
                    return BadRequest("Failed to update the Pump");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //DELETE PUMP//  
        [AllowAnonymous]
        [HttpDelete("{PumpId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> DeletePump(Guid PumpId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = await _pumpRepository.DeletePump(PumpId);
                    if (result)
                    {
                        return BadRequest("User deletion has failed");
                    }
                    else
                    {
                        return Ok("User has been successfully deleted");
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

        //CREATE PUMP DESCRIPTION
        // [HttpPost]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]
        // public IActionResult CreatePumpDesc([FromQuery] Guid PumpId, [FromQuery] PumpDto pumpdescription)
        // {
        //     if(pumpdescription == null)
        //         return BadRequest(ModelState);

        //     var desc = _pumpRepository.GetPumps()
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

        //UPDATE PUMP DESCRIPTION

        //DELETE PUMP DESCRIPTION
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

        //CREATE PUMP// 
        // [HttpPost]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]

        // public IActionResult CreatePump([FromQuery] Guid PumpId, /*[FromQuery] int categoryId,*/ [FromBody] PumpDto pumpCreate)
        // {
        //     if(pumpCreate == null)
        //         return BadRequest(ModelState);

        //     // var pump = _pumpRepository.GetPumps().
        //     // Where(c => c.PumpName.Trim().ToUpper() == pumpCreate.PumpName.TrimEnd().ToUpper()).FirstOrDefault();
        //     //Where(c => c.LastName.Trim().ToUpper() == WheelCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();


        //     // if(pump != null)
        //     // {
        //     //     ModelState.AddModelError("", "Pump Already Exists");
        //     //     return StatusCode(422, ModelState);
        //     // }

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var pumpMap = _mapper.Map<Pump>(pumpCreate);

        //     if(!_pumpRepository.CreatePump(PumpId, /*categoryId,*/ pumpMap))
        //     {
        //         ModelState.AddModelError("","Something went wrong while saving");
        //         return StatusCode(500, ModelState);
        //     }

        //     return Ok("Successfully Created");

        // }
    }
}