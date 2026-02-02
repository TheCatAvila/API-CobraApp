namespace API_CobraApp.Application.Dtos.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string LinkedCode { get; set; } = null!;
    }

}
