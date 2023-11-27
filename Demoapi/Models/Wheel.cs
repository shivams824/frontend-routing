using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Demoapi.Models
{
    public class Wheel : BaseModel
    {
        public string WheelName { get; set; }  
        public string Type { get; set; }    
        public bool WheelStatus { get; set; }  
        public string WheelDescription { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
