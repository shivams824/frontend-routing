using Demoapi.Data;
using Demoapi.Interface;
using Demoapi.Models;

namespace Demoapi.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        public AdminRepository(DataContext context)
        {
            _context = context;           
        }

        public bool AdminExists(Guid existId)
        {
            return _context.Admins.Any(e => e.AdminId == existId);
        }

        public Admin GetAdmin(Guid Id)
        {
            return _context.Admins.Where(p => p.AdminId == Id).FirstOrDefault();
        }

        public Admin GetAdmin(string AdminName)
        {
            return _context.Admins.Where(p => p.Name == AdminName).FirstOrDefault();
        }

        public ICollection<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();
            return save > 0 ? true : false ;
        }
    }
}