namespace API_CobraApp.Domain.Entities
{
    public class ExternalProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; } = null!;
        public bool IsEnabled { get; set; } = true;

        public ICollection<UserExternal> UserExternals { get; set; }
            = new List<UserExternal>();
    }
}