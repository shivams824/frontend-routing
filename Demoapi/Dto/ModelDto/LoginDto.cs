using System.ComponentModel.DataAnnotations;

namespace Demoapi.Authentication
{
    public class LoginDto
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}