namespace Practice.Dto
{
    public class CreateUserDtoModel
    {
        //Required to add validations in all of the DTo files that are used for API requests.
        public string UserName { get; set; }
        public string Email {get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}