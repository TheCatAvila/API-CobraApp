using API_CobraApp.Infrastructure.Persistence;
using API_CobraApp.Domain.Entities;
using API_CobraApp.Application.Dtos.Auth;
using API_CobraApp.Application.Dtos.Users;
using Google.Apis.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;
using API_CobraApp.Application.Common.Interfaces;

namespace API_CobraApp.Application.Features.Auth.ExternalProviders.Google
{
    public class GoogleLoginHandler
    : IRequestHandler<GoogleLoginCommand, AuthResponseDto>
    {
        private readonly AppDbContext _db;
        private readonly IJwtService _jwtService;

        public GoogleLoginHandler(
            AppDbContext db,
            IJwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public async Task<AuthResponseDto> Handle(
            GoogleLoginCommand request,
            CancellationToken cancellationToken)
        {
            // 1️⃣ Validar token con Google
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                request.IdToken);

            var googleId = payload.Subject; // sub
            var email = payload.Email;
            var firstName = payload.GivenName ?? "";
            var lastName = payload.FamilyName ?? "";

            // 2️⃣ Buscar provider Google
            var provider = await _db.ExternalProviders
                .FirstOrDefaultAsync(x => x.ProviderName == "Google",
                    cancellationToken);

            if (provider == null || !provider.IsEnabled)
                throw new Exception("Google login is disabled.");

            // 3️⃣ Buscar si ya está vinculado
            var userExternal = await _db.UserExternals
                .Include(x => x.User)
                .FirstOrDefaultAsync(x =>
                    x.ExternalProviderId == provider.Id &&
                    x.ProviderUserId == googleId,
                    cancellationToken);

            User? user;

            if (userExternal != null)
            {
                user = userExternal.User;
            }
            else
            {
                // 4️⃣ Buscar por email
                user = await _db.Users
                    .FirstOrDefaultAsync(x => x.Email == email,
                        cancellationToken);

                if (user == null)
                {
                    // 5️⃣ Crear usuario nuevo
                    user = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        LinkedCode = "GOOGLE", // puedes mejorar esto
                        PasswordHash = null
                    };

                    _db.Users.Add(user);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                // 6️⃣ Vincular cuenta
                var newLink = new UserExternal
                {
                    UserId = user.Id,
                    ExternalProviderId = provider.Id,
                    ProviderUserId = googleId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _db.UserExternals.Add(newLink);
                await _db.SaveChangesAsync(cancellationToken);
            }

            // 7️⃣ Generar JWT
            var token = _jwtService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                }
            };
        }
    }
}
