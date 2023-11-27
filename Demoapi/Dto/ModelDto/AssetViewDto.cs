using System.Reflection;

namespace Practice.Dto
{
    public class AssetViewDto
    {
        public Guid Id { get; set; }
        public string AssetName { get; set; }
        public string type { get; set; }
        public bool Status { get; set; }
        public string  AllDescription { get; set; }
    }
}