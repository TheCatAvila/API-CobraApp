namespace API_CobraApp.Domain.Entities
{
    public class UserExternal
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ExternalProviderId { get; set; }
        public ExternalProvider ExternalProvider { get; set; } = null!;

        public string ProviderUserId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}