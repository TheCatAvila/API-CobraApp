using API_CobraApp.Application.Dtos.Users;
using API_CobraApp.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API_CobraApp.Application.Features.Users.GetAll
{
    public class GetUsersHandler
        : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly AppDbContext _db;

        public GetUsersHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserDto>> Handle(
            GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            return await _db.Users
                .AsNoTracking()
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync(cancellationToken);
        }
    }
}