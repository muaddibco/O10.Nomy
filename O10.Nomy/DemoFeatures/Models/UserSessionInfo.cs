namespace O10.Nomy.DemoFeatures.Models
{
    public record UserSessionInfo
    {
        public string SessionKey { get; set; }
        public long UserId { get; set; }
    }
}
