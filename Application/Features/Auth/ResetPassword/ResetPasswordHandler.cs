using MediatR;

namespace API_CobraApp.Application.Features.Auth.ResetPassword;

public class ResetPasswordHandler
    : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // TODO:
        // - Validar código
        // - Cambiar contraseña
        // - Invalidar código

        await Task.CompletedTask;
    }
}