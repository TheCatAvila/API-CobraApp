using API_CobraApp.Application.Dtos.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using API_CobraApp.Infrastructure.Persistence;

namespace API_CobraApp.Application.Features.Users.Update
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly AppDbContext _context;

        public UpdateUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException("Usuario no encontrado");

            // Actualizamos campos
            user.FirstName = request.Dto.FirstName;
            user.LastName = request.Dto.LastName;
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