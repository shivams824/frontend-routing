using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Demoapi.Dto;
using Demoapi.Interface;
using Demoapi.Models;
using Practice.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Demoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

//GET ALL ADMIN//
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Admin>))]
       
        public IActionResult GetAdmins()
        {
            var admins = _mapper.Map<List<AdminDto>>(_adminRepository.GetAdmins());

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(admins);

        }


//GET ADMIN BY ID//
        [HttpGet("{AdminId}")]
        [ProducesResponseType(200, Type = typeof(Admin))]
        [ProducesResponseType(400)]

        public IActionResult GetAdmin(Guid adminId)
        {
            if(!_adminRepository.AdminExists(adminId))
                return NotFound();
            
            var admin = _mapper.Map<AdminDto>(_adminRepository.GetAdmin(adminId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(admin);
        }
    }
}


//CREATE ADMIN// 
        // [HttpPost]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(400)]

        // public IActionResult CreateAdmin([FromQuery] Guid adminId, /*[FromQuery] int categoryId,*/ [FromBody] AdminDto adminCreate)
        // {
        //     if(adminCreate == null)
        //         return BadRequest(ModelState);

        //     var admin = _adminRepository.GetAdmins().
        //     Where(c => c.Name.Trim().ToUpper() == adminCreate.Name.TrimEnd().ToUpper()).FirstOrDefault();
        //     //Where(c => c.LastName.Trim().ToUpper() == WheelCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
                                
 
        //     if(admin != null)
        //     {
        //         ModelState.AddModelError("", "Admin Already Exists");
        //         return StatusCode(422, ModelState);
        //     }

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var adminMap = _mapper.Map<Admin>(adminCreate);

        //     if(!_adminRepository.CreateAdmin(AdminId, /*categoryId,*/ adminMap))
        //     {
        //         ModelState.AddModelError("","Something went wrong while saving");
        //         return StatusCode(500, ModelState);
        //     }

        //     return Ok("Successfully Created");

        // }
// //UPDATE ADMIN/
//         [HttpPut("{/updateadmin}")]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(404)]

//         public IActionResult UpdateAdmin(Guid adminId, [FromQuery] AdminDto updateadmin)
//         {
//             if(updateadmin == null)
//                 return BadRequest(ModelState);
            
//             if (AdminId != updateadmin.AdminId)
//                 return BadRequest(ModelState);

//             if (!_adminRepository.AdminExists(AdminId))
//                 return NotFound();

//             if (!ModelState.IsValid)
//                 return BadRequest();

//             var adminMap = _mapper.Map<Admin>(updateadmin);

//             if(!_adminRepository.UpdateAdmin(AdminId, adminMap))
//             {
//                 ModelState.AddModelError("", "Something went wrong while updating status");
//                 return StatusCode(500, ModelState);
//             }
//             return NoContent();
        // }


//DELETE ADMIN//  

        // [HttpDelete("{AdminId}")]
        // [ProducesResponseType(400)]
        // [ProducesResponseType(204)]
        // [ProducesResponseType(404)]

        // public IActionResult DeleteAdmin(Guid adminId)
        // {
        //     if(!_adminRepository.AdminExists(AdminId))
        //         return NotFound();
            
        //     var admindelete = _adminRepository.GetAdmin(AdminId);

        //     if(!ModelState.IsValid)
        //         return BadRequest(ModelState);
            
        //     if(!_adminRepository.DeleteAdmin(admindelete))
        //     {
        //         ModelState.AddModelError("", "Something went wrong while deleting the Wheel");

        //     }

        //     return NoContent();
        // }

