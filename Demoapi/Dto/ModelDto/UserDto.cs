namespace Practice.Dto
{
    public class UserDto
    {
        public string UserId {get; set;}
        public string Token { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string Email {get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool EmailConfirmed { get; internal set; }
    }
}