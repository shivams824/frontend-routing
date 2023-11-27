using System.Data;
using Demoapi.Data;
using Demoapi.Interface;
using Demoapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Dto;

namespace Demoapi.Repository
{
    public class WheelRepository : IWheelRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public WheelRepository(DataContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateWheel(CreateWheelDtoModel requestBody)
        {
            try
            {
                Wheel wheel = new Wheel
                {
                    UserId ="dbee5087-9968-48b5-961e-bb77443bea52",
                    Id = requestBody.WheelId,
                    WheelName = requestBody.WheelName,
                    WheelStatus = requestBody.WheelStatus,
                    Type = requestBody.Type,
                    WheelDescription = "added",
                };
                _context.Wheels.Add(wheel);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception", ex.Message);
                return false;
            }
        }

        public async Task<Wheel> GetWheel(Guid Id)
        {
            return await _context.Wheels.Where(p => p.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Wheel> GetWheelByName(string Name)
        {
            return await _context.Wheels.Where(p => p.WheelName == Name).FirstOrDefaultAsync();
        }

        public async Task<Wheel> WheelExists(Guid wheelId)
        {
            return await _context.Wheels.Where(e => e.Id == wheelId).FirstOrDefaultAsync();
        }
        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false;
        }

        public async Task<List<Wheel>> GetWheelsByUser(string UserId)
        {
            return await _context.Wheels.Where(u => u.UserId == UserId).ToListAsync();
        }

        public bool UpdateWheel(Guid WheelId, WheelUpdateDto updatewheel)
        {
            try
            {
                var prent = _context.Wheels.Find(WheelId);

                prent.WheelStatus = updatewheel.WheelStatus;
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

        public bool CreateWheelDesc(Wheel wheeldesc)
        {
            _context.Add(wheeldesc);
            return Save();
        }

        public bool UpdateWheelDesc(Wheel wheeldesc)
        {
            _context.Update(wheeldesc);
            return Save();
        }

        public bool DeleteWheelDesc(Wheel wheeldesc)
        {
            _context.Remove(wheeldesc);
            return Save();
        }

        public async Task<List<Wheel>> GetWheels()
        {
            return await _context.Wheels.ToListAsync();
        }

        public async Task<bool> DeleteWheel(Guid WheelId)
        {
            try
            {
                var wheel = await _context.Wheels.FindAsync(WheelId);
                if (wheel != null)
                {
                    _context.Wheels.Remove(wheel);
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