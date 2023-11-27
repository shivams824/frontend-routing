namespace Practice.Dto
{
    public class PumpResponseDto
    {
        public Guid PumpId { get; set; }
        public string PumpName { get; set; }   
        public string Type { get; set; }   
        public bool PumpStatus { get; set; }  
        public string PumpDescription { get; set; }
    }
}