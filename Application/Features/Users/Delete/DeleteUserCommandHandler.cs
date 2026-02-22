using MediatR;
using Microsoft.EntityFrameworkCore;
using API_CobraApp.Infrastructure.Persistence;

namespace API_CobraApp.Application.Features.Users.Delete
{
    public class DeleteUserCommandHandler
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly AppDbContext _context;

        public DeleteUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(
            DeleteUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException("User not found");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}