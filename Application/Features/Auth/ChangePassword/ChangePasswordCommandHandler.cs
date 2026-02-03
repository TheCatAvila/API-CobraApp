using MediatR;
using Microsoft.EntityFrameworkCore;
using API_CobraApp.Infrastructure.Persistence;
using BCrypt.Net;

namespace API_CobraApp.Application.Features.Auth.ChangePassword
{
    public class ChangePasswordCommandHandler
        : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly AppDbContext _db;

        public ChangePasswordCommandHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user is null || string.IsNullOrEmpty(user.PasswordHash))
                throw new UnauthorizedAccessException();

            // 1️⃣ Verificar password actual
            var validPassword = BCrypt.Net.BCrypt.Verify(
                request.Dto.CurrentPassword,
                user.PasswordHash
            );

            if (!validPassword)
                throw new UnauthorizedAccessException("Contraseña actual incorrecta");

            // 2️⃣ Hashear nueva password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(
                request.Dto.NewPassword
            );

            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}