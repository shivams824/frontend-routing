using Demoapi.Models;

namespace Demoapi.Interface
{
    public interface IJsonRepository
    {
        bool JsonExists(Guid JsonId);
        bool CreateJson(jsondata json /*other id*/);
        bool Save();
        bool DeleteJson(Guid JsonId);
        bool UpdateJson(Guid JsonId);
        jsondata GetJson(Guid JsonId);
    }
}