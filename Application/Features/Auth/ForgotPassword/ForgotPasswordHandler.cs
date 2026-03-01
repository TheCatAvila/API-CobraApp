using API_CobraApp.Application.Common.Interfaces;
using API_CobraApp.Domain.Entities;
using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace API_CobraApp.Application.Features.Auth.ForgotPassword;

public class ForgotPasswordHandler
    : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly AppDbContext _db;
    private readonly IEmailSender _emailSender;

    public ForgotPasswordHandler(AppDbContext db, IEmailSender emailSender)
    {
        _db = db;
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user == null)
            return Unit.Value;

        // 1) Generar código
        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        // 2) Guardar código en DB / cache (ejemplo)
        var reset = new PasswordReset
        {
            UserId = user.Id,
            Code = code,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        };

        _db.PasswordResets.Add(reset);
        await _db.SaveChangesAsync(cancellationToken);

        // 3) Enviar email
        await _emailSender.SendPasswordResetCode(email, code);

        return Unit.Value;
    }
}