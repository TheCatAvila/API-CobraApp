using MediatR;
using API_CobraApp.Application.Dtos.Auth;

namespace API_CobraApp.Application.Features.Auth.ExternalProviders.Google
{
    public record GoogleLoginCommand(string IdToken)
    : IRequest<AuthResponseDto>;
}
