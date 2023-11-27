using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demoapi.Data;
using Demoapi.Interface;
using Microsoft.EntityFrameworkCore;
using Practice.Dto;

namespace Demoapi.Repository
{
    public class AssetRepository : IAssetRepository
    {
        private readonly DataContext _context;

        public AssetRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<AssetViewDto>> GetAssets()
        {
            var assetList = new List<AssetViewDto>();

            var pumps = _context.Pumps.Select(x => new AssetViewDto
            {
                Id = x.Id,
                AssetName = x.PumpName,
                type = x.Type,
                Status = x.PumpStatus,
                AllDescription = x.Description,
            });

            var cameras = _context.Cameras.Select(x => new AssetViewDto
            {
                Id = x.Id,
                AssetName = x.CameraName,
                type = x.Type,
                Status = x.CameraStatus,
                AllDescription = x.CameraDescription,
            });

            var wheels = _context.Wheels.Select(x => new AssetViewDto
            {
                Id = x.Id,
                AssetName = x.WheelName,
                type = x.Type,
                Status = x.WheelStatus,
                AllDescription = x.WheelDescription,
            });

            assetList.AddRange(pumps);
            assetList.AddRange(cameras);
            assetList.AddRange(wheels);

            return assetList;
        }

        public async Task<List<AssetViewDto>> GetAssetsByUserId(string userId)
        {
            var assetList = new List<AssetViewDto>();

            var pumps = _context.Pumps
                .Where(p => p.UserId == userId)
                .Select(x => new AssetViewDto
                {
                    Id = x.Id,
                    AssetName = x.PumpName,
                    type = x.Type,
                    Status = x.PumpStatus,
                    AllDescription = x.Description,
                });

            var cameras = _context.Cameras
                .Where(c => c.UserId == userId)
                .Select(x => new AssetViewDto
                {
                    Id = x.Id,
                    AssetName = x.CameraName,
                    type = x.Type,
                    Status = x.CameraStatus,
                    AllDescription = x.CameraDescription,
                });

            var wheels = _context.Wheels
                .Where(w => w.UserId == userId)
                .Select(x => new AssetViewDto
                {
                    Id = x.Id,
                    AssetName = x.WheelName,
                    type = x.Type,
                    Status = x.WheelStatus,
                    AllDescription = x.WheelDescription,
                });

            assetList.AddRange(await pumps.ToListAsync());
            assetList.AddRange(await cameras.ToListAsync());
            assetList.AddRange(await wheels.ToListAsync());

            return assetList;
        }
    }
}
