namespace Practice.Dto
{
    public class CreateCameraDtoModel
    {
        public Guid CameraId { get; set; }
        public string CameraName { get; set; }   
        public string Type { get; set; }   
        public bool CameraStatus { get; set; } 
    }
}