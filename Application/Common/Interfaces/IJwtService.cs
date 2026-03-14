namespace API_CobraApp.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(API_CobraApp.Domain.Entities.User user);
    }
}