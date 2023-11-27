using Demoapi.Models;
using Practice.Dto;

namespace Demoapi.Interface
{
    public interface IWheelRepository
    {
        public Task<List<Wheel>> GetWheels();
        public Task<Wheel> GetWheel(Guid WheelId);
        public Task<Wheel> GetWheelByName(string WheelName);
        public Task<List<Wheel>> GetWheelsByUser(string UserId);
        Task<Wheel> WheelExists(Guid WheelId);
        // public Task<Wheel> WheelExists(Guid WheelId, WheelUpdateDto wheel);
        // bool CreateWheel(Guid WheelId, Wheel wheel/*other id*/);
        public Task<bool> CreateWheel(CreateWheelDtoModel requestBody);
        // bool DeleteWheel(/*Guid WheelId,*/ Wheel wheel);
        Task<bool> DeleteWheel(Guid WheelId);
        public bool UpdateWheel(Guid WheelId, WheelUpdateDto wheel);
        bool CreateWheelDesc(Wheel wheel);
        bool UpdateWheelDesc(Wheel wheel);
        bool DeleteWheelDesc(Wheel wheel);
        bool Save();
    }
}