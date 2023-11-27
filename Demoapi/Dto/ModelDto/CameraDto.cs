using System.ComponentModel.DataAnnotations;

namespace Practice.Dto
{
    public class CameraDto
    {
        public Guid CameraId {get; set;}
        public string CameraName { get; set; }
        public string Type { get; set; }
        public bool CameraStatus { get; set; }  
        public string CameraDescription { get; set; }
        public string UserId { get; set; } 
    }
}