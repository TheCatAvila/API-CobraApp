using API_CobraApp.Application.Dtos.Users;
using API_CobraApp.Domain.Entities;
using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using BCrypt.Net;

namespace API_CobraApp.Application.Features.Users.Create
{
    public class CreateUserHandler
        : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly AppDbContext _db;

        public CreateUserHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            string? hashedPassword = null;

            // Solo hasheamos si viene password
            if (!string.IsNullOrWhiteSpace(request.User.Password))
            {
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(
                    request.User.Password
                );
            }

            var user = new User
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = request.User.Email,
                LinkedCode = request.User.LinkedCode,
                PasswordHash = hashedPassword
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}