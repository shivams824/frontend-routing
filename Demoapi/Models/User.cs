using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

namespace Demoapi.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public ICollection<Pump> Pumps { get; set; }
        public ICollection<Wheel> Wheels { get; set; }
        public ICollection<Camera> Cameras { get; set;}
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}