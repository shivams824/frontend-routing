using System.ComponentModel.DataAnnotations;

namespace Demoapi.Models
{
    public class Camera : BaseModel
    { 
        public string CameraName { get; set; }  
        public string Type { get; set; }    
        public bool CameraStatus { get; set; }  
        public string CameraDescription { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        
    }
}