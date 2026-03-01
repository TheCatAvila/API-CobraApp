using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_CobraApp.Application.Features.Auth.ResetPassword;

public class ResetPasswordHandler
    : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly AppDbContext _db;

    public ResetPasswordHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Unit> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user == null)
            throw new Exception("Usuario no encontrado");

        var reset = await _db.PasswordResets
            .Where(x =>
                x.UserId == user.Id &&
                x.Code == request.Code &&
                !x.Used)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (reset == null)
            throw new Exception("Código inválido");

        if (DateTime.UtcNow > reset.ExpiresAt)
            throw new Exception("Código expirado");

        // 🔐 Hashear nueva contraseña
        string? hashedPassword = null;

        if (!string.IsNullOrWhiteSpace(request.NewPassword))
        {
            hashedPassword = BCrypt.Net.BCrypt.HashPassword(
                request.NewPassword
            );
        }

        user.PasswordHash = hashedPassword;
        reset.Used = true;

        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}