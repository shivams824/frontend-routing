using System.ComponentModel.DataAnnotations;

namespace Demoapi.Models
{
    public class Pump : BaseModel
    {
        public string PumpName { get; set; }    
        public bool PumpStatus { get; set; }
        public string Type { get; set; }  
        public string Description { get; set; }
        public User User { get; set; } 
        public string UserId {get; set; }
    }
}