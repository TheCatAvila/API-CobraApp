using API_CobraApp.Application.Dtos.Auth;
using MediatR;

namespace API_CobraApp.Application.Features.Auth.ChangePassword
{
    public record ChangePasswordCommand(
        int UserId,
        ChangePasswordDto Dto
    ) : IRequest<Unit>;
}