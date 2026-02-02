using API_CobraApp.Application.Dtos.Users;
using MediatR;

namespace API_CobraApp.Application.Features.Users.GetById
{
    public record GetUserByIdQuery(int Id) : IRequest<UserDto?>;
}