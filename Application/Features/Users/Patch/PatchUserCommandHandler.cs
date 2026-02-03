using MediatR;
using Microsoft.EntityFrameworkCore;
using API_CobraApp.Infrastructure.Persistence;
using API_CobraApp.Application.Dtos.Users;

namespace API_CobraApp.Application.Features.Users.Patch
{
    public class PatchUserCommandHandler
        : IRequestHandler<PatchUserCommand, UserDto>
    {
        private readonly AppDbContext _context;

        public PatchUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(
            PatchUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException("Usuario no encontrado");

            // 🔁 Update parcial
            if (request.Dto.FirstName is not null)
                user.FirstName = request.Dto.FirstName;

            if (request.Dto.LastName is not null)
                user.LastName = request.Dto.LastName;

            if (request.Dto.Email is not null)
                user.Email = request.Dto.Email;

            await _context.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}