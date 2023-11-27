using System.Data;
using Demoapi.Data;
using Demoapi.Interface;
using Demoapi.Models;
using Microsoft.EntityFrameworkCore;
using Practice.Dto;

namespace Demoapi.Repository
{
    public class PumpRepository : IPumpRepository
    {
        private readonly DataContext _context;
        public PumpRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<bool> CreatePump(CreatePumpDtoModel requestBody)
        {
            try
            {
                
                // jsondata data = new jsondata {
                //     JsonId = requestBody.PumpId,
                // };
                Pump pump = new Pump
                {   UserId = "dbee5087-9968-48b5-961e-bb77443bea52",
                    PumpName = requestBody.PumpName,
                    PumpStatus = requestBody.PumpStatus,
                    Id = requestBody.PumpId,
                    Type = requestBody.Type,
                    Description = "added",
                };
                _context.Pumps.Add(pump);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex.Message);
                return false;
            }
        }

        public bool CreatePumpDesc(/*Guid PumpId,*/ Pump pumpdesc)
        {
            _context.Add(pumpdesc);
            return Save();
        }

        public bool DeletePumpDesc(/*Guid PumpId,*/ Pump pumpdesc)
        {
            _context.Remove(pumpdesc);
            return Save();
        }

        public async Task<Pump> GetPump(Guid Id)
        {
            return await _context.Pumps.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Pump> GetPumpByName(string Name)
        {
            return await _context.Pumps.Where(p => p.PumpName == Name).FirstOrDefaultAsync();
        }

        public async Task<List<Pump>> GetPumps()
        {
            return await _context.Pumps.ToListAsync();
        }

        public async Task<Pump> PumpExists(Guid pumpId)
        {
            return await _context.Pumps.Where(e => e.Id == pumpId).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }


        public bool UpdatePumpDesc(/*Guid PumpId,*/ Pump pumpdesc)
        {
            _context.Update(pumpdesc);
            return Save();
        }

        public async Task<List<Pump>> GetPumpsByUser(string UserId)
        {
            return await _context.Pumps.Where(u => u.UserId == UserId).ToListAsync();
        }

        public bool UpdatePump(Guid PumpId, PumpUpdateDto updatepump)
        {
            try
            {
                var prent = _context.Pumps.Find(PumpId);

                if (prent != null)
                {
                    prent.PumpStatus = updatepump.PumpStatus;
                    _context.Pumps.Update(prent);
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

        public async Task<bool> DeletePump(Guid PumpId)
        {
            try
            {
                var pump = await _context.Pumps.FindAsync(PumpId);
                if (pump != null)
                {
                    _context.Pumps.Remove(pump);
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
    }
}