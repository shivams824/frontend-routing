using System.Data;
using Demoapi.Data;
using Demoapi.Interface;
using Demoapi.Models;
using Microsoft.EntityFrameworkCore;
using Practice.Dto;

namespace Demoapi.Repository
{
    public class CameraRepository : ICameraRepository
    {
        private readonly DataContext _context;
        public CameraRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCamera(CreateCameraDtoModel createcamera)
        {
            try
            {
                Camera camera = new Camera
                {   UserId = "dbee5087-9968-48b5-961e-bb77443bea52",
                    CameraName = createcamera.CameraName,
                    CameraStatus = createcamera.CameraStatus,
                    Type = createcamera.Type,
                    Id = createcamera.CameraId,
                    CameraDescription = "added",
                };
                   _context.Cameras.Add(camera);
                   await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCamera(Guid CameraId)
        {
            try
            {
                var camera = await _context.Cameras.FindAsync(CameraId);
                if (camera != null)
                {
                    _context.Cameras.Remove(camera);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex.Message);
                return false;
            }
        }

        public async Task<Camera> GetCamera(Guid cameraId)
        {
            return await _context.Cameras.Where(p => p.Id == cameraId).FirstOrDefaultAsync();
        }

        public async Task<Camera> GetCameraByName(string Name)
        {
            return await _context.Cameras.Where(p => p.CameraName == Name).FirstOrDefaultAsync();
        }

        public async Task<List<Camera>> GetCameras()
        {
            return await _context.Cameras.ToListAsync();
        }

        public async Task<Camera> CameraExists(Guid cameraId)
        {
            return await _context.Cameras.Where(e => e.Id == cameraId).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public bool CreateCamDesc(Camera camdesc)
        {
            _context.Add(camdesc);
            return Save();
        }

        public bool UpdateCamDesc(Camera camdesc)
        {
            _context.Update(camdesc);
            return Save();
        }

        public bool DeleteCamDesc(Camera camdesc)
        {
            _context.Remove(camdesc);
            return Save();
        }

        public bool CameraDesc(Guid cameraId)
        {
            return _context.Cameras.Any(e => e.Id == cameraId);
        }

        public bool UpdateCamera(Guid CameraId, CameraUpdateDto updatecamera)
        {
            try
            {
                var prent = _context.Cameras.Find(CameraId);

                prent.CameraStatus = updatecamera.CameraStatus;
                if (prent != null)
                { 
                    _context.Update(prent);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex.Message);
                return false;
            }
        }

        public async Task<List<Camera>> GetCamerasByUser(string UserId)
        {
            return await _context.Cameras.Where(u => u.UserId == UserId).ToListAsync();
        }
    }
}