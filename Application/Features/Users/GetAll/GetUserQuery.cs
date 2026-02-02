using API_CobraApp.Application.Dtos.Users;
using MediatR;

namespace API_CobraApp.Application.Features.Users.GetAll
{
    public record GetUsersQuery : IRequest<List<UserDto>>;
}