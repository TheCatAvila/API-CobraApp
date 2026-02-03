using API_CobraApp.Application.Dtos.Users;
using MediatR;

namespace API_CobraApp.Application.Features.Users.Patch
{
    public record PatchUserCommand(
        int Id,
        PatchUserDto Dto
    ) : IRequest<UserDto>;
}