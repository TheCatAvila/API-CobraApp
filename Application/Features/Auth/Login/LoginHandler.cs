using API_CobraApp.Application.Dtos.Auth;
using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_CobraApp.Application.Features.Auth.Login
{
    public class LoginHandler
        : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public LoginHandler(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<AuthResponseDto> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == request.Login.Email);

            if (user == null || user.PasswordHash == null)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var isValid = BCrypt.Net.BCrypt.Verify(
                request.Login.Password,
                user.PasswordHash
            );

            if (!isValid)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var token = GenerateJwt(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        private string GenerateJwt(Domain.Entities.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email),
                new Claim("linkedCode", user.LinkedCode)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}