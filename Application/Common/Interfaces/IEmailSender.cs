namespace API_CobraApp.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendPasswordResetCode(string email, string code);
    }
}