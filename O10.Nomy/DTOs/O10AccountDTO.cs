namespace O10.Nomy.DTOs
{
    public class O10AccountDTO
    {
        public long AccountId { get; set; }
        public AccountType AccountType { get; set; }
        public string AccountInfo { get; set; }
        public string? PublicViewKey { get; set; }
        public string? PublicSpendKey { get; set; }
    }
}
