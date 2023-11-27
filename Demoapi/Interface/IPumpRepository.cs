using Demoapi.Models;
using Practice.Dto;

namespace Demoapi.Interface
{
    public interface IPumpRepository
    {
        public Task<List<Pump>> GetPumps();
        public Task<Pump> GetPump(Guid PumpId);
        public Task<Pump> GetPumpByName(string PumpName);
        public Task<List<Pump>> GetPumpsByUser(string UserId);
        Task<Pump> PumpExists(Guid PumpId);
        // bool CreatePump(Guid PumpId, Pump pump/*other id*/);
        public Task<bool> CreatePump(CreatePumpDtoModel requestBody);
        // bool DeletePump(/*Guid PumpId,*/ Pump pump);
        Task<bool> DeletePump(Guid PumpId);
        public bool UpdatePump(Guid PumpId, PumpUpdateDto pump);
        bool CreatePumpDesc(Pump pump);
        bool UpdatePumpDesc(Pump pump);
        bool DeletePumpDesc(Pump pump);
        bool Save();   
    }
}