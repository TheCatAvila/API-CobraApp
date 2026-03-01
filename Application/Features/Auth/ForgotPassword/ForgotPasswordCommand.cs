using MediatR;

namespace API_CobraApp.Application.Features.Auth.ForgotPassword;

public record ForgotPasswordCommand(string Email) : IRequest<Unit>;