using Demoapi.Models;
using Practice.Dto;

namespace Demoapi.Interface
{
    public interface ICameraRepository
    {
        public Task<List<Camera>> GetCameras();
        public Task<Camera> GetCamera(Guid CameraId);
        public Task<Camera> GetCameraByName(string CameraName);
        public Task<List<Camera>> GetCamerasByUser(string UserId);
        Task<Camera> CameraExists(Guid CameraId);
        // bool CreateCamera(Guid CameraId, Camera camera/*other id*/);
        public Task<bool> CreateCamera(CreateCameraDtoModel requestBody);
        // bool DeleteCamera(/*Guid CameraId,*/ Camera camera);
        Task<bool> DeleteCamera(Guid CameraId);
        public bool UpdateCamera(Guid CameraId, CameraUpdateDto camera);
        bool CreateCamDesc(Camera camera);
        bool UpdateCamDesc(Camera camera);
        bool DeleteCamDesc(Camera camera);
        bool Save();   
    }
}