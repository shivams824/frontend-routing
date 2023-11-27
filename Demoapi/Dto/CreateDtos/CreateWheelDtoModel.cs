namespace Practice.Dto
{
    public class CreateWheelDtoModel
    {
        public Guid WheelId { get; set; }
        public string WheelName { get; set; }   
        public string Type { get; set; }   
        public bool WheelStatus { get; set; } 
        public string UserId { get; set; }
    }
}