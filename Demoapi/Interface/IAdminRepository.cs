using Demoapi.Models;

namespace Demoapi.Interface
{
    public interface IAdminRepository
    {
        public ICollection<Admin> GetAdmins();
        Admin GetAdmin(Guid AdminId);
        Admin GetAdmin(string AdminName);
        bool AdminExists(Guid AdminId);
        // bool CreateAdmin(Guid adminId, Admin admin);
        // bool DeleteAdmin(/*Guid adminId,*/ Admin admin);
        // bool UpdateAdmin(Guid adminId);
        bool Save();
    }
}