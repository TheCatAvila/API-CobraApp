using API_CobraApp.Application.Dtos.Users;
using MediatR;

namespace API_CobraApp.Application.Features.Users.Update
{
    public record UpdateUserCommand(
        int Id,
        UpdateUserDto Dto
    ) : IRequest<UserDto>;
}