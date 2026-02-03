using MediatR;

namespace API_CobraApp.Application.Features.Auth.ForgotPassword;

public class ForgotPasswordHandler
    : IRequestHandler<ForgotPasswordCommand>
{
    public async Task Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // TODO:
        // - Buscar usuario
        // - Generar código
        // - Guardar código + expiración
        // - Enviar SMS / email

        var code = new Random().Next(100000, 999999);
        System.Diagnostics.Debug.WriteLine($"Código recuperación: {code}");

        await Task.CompletedTask;
    }
}