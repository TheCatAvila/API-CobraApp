using MediatR;

namespace API_CobraApp.Application.Features.Users.Delete
{
    public record DeleteUserCommand(int Id) : IRequest<Unit>;
}