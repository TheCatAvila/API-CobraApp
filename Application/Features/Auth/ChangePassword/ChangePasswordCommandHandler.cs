using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_CobraApp.Application.Features.Auth.ChangePassword;

public class ChangePasswordHandler
    : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly AppDbContext _db;

    public ChangePasswordHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

        if (user == null)
            throw new Exception("Usuario no encontrado");

        // 🔎 Verificar contraseña actual
        var isValid = BCrypt.Net.BCrypt.Verify(
            request.Dto.CurrentPassword,
            user.PasswordHash);

        if (!isValid)
            throw new Exception("La contraseña actual es incorrecta");

        if (string.IsNullOrWhiteSpace(request.Dto.NewPassword))
            throw new Exception("Nueva contraseña inválida");

        // 🔐 Generar nuevo hash
        var newHashedPassword =
            BCrypt.Net.BCrypt.HashPassword(request.Dto.NewPassword);

        user.PasswordHash = newHashedPassword;

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}