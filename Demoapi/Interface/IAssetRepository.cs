using System.Collections;
using Demoapi.Models;
using Practice.Dto;

namespace Demoapi.Interface
{
    public interface IAssetRepository
    {
        public Task<List<AssetViewDto>> GetAssets();
        public  Task<List<AssetViewDto>> GetAssetsByUserId(string userId);
    }
}