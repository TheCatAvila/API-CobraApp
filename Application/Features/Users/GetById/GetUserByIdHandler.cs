using API_CobraApp.Application.Dtos.Users;
using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_CobraApp.Application.Features.Users.GetById
{
    public class GetUserByIdHandler
        : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly AppDbContext _db;

        public GetUserByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto?> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == request.Id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}