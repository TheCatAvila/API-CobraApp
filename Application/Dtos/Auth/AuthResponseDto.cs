using API_CobraApp.Application.Dtos.Users;

namespace API_CobraApp.Application.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }
}