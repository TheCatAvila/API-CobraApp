namespace API_CobraApp.Domain.Entities
{
    public class PasswordReset
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string Code { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public bool Used { get; set; }
    }
}