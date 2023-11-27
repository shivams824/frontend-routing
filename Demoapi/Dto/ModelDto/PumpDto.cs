namespace Practice.Dto
{
    public class PumpDto
    {
        public Guid PumpId { get; set; }
        public string PumpName { get; set; } 
        public string Type { get; set; }  
        public bool PumpStatus { get; set; }  
        public string Description { get; set; }
        public string UserId { get; set; } 
    }
}