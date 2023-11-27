using System.ComponentModel.DataAnnotations;

namespace Demoapi.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
        public BaseModel()
        {
            Id = Guid.NewGuid();
        }

    }
}