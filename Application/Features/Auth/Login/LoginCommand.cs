using API_CobraApp.Application.Dtos.Auth;
using MediatR;

public record LoginCommand(LoginDto Login)
    : IRequest<AuthResponseDto>;