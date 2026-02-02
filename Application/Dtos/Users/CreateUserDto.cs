namespace API_CobraApp.Application.Dtos.Users
{
    public class CreateUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string LinkedCode { get; set; } = null!;
    }

}
