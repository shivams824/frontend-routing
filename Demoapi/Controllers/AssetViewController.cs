using Microsoft.AspNetCore.Mvc;
// using Demoapi.Dto;
using Demoapi.Interface;
using Demoapi.Models;
using Practice.Dto;
using Microsoft.AspNetCore.Authorization;
using Demoapi.Repository;

namespace Demoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [AllowAnonymous]
    public class AssetViewController : Controller
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IUserRepository _userRepository;

        public AssetViewController(IAssetRepository assetRepository, IUserRepository userRepository)
        {
            _assetRepository = assetRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AssetViewDto>))]

        public async Task<ActionResult<List<AssetViewDto>>> GetAssets()
        {

            try
            {
                var asset = await _assetRepository.GetAssets();
                if (asset.Any())
                {
                    return Ok(asset);
                }
                else
                {
                    return NotFound("No Assets Found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{UserId}/assets")]
        [ProducesResponseType(200, Type = typeof(List<AssetViewDto>))]
        [ProducesResponseType(400)]

        public async Task<ActionResult> GetAssetsByUser(string UserId)
        {
            if (ModelState.IsValid)
            {
                var exist = await _userRepository.UserExists(UserId);
                if (exist != null)
                {
                    var assets = await _assetRepository.GetAssetsByUserId(UserId);
                    return Ok(assets);
                }
                else
                {
                    return NotFound("No assets exist for the specified User");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }

}