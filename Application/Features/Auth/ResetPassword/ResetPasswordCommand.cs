using MediatR;

namespace API_CobraApp.Application.Features.Auth.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string Code,
    string NewPassword
) : IRequest<Unit>;