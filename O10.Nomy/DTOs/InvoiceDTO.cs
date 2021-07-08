namespace O10.Nomy.DTOs
{
    public class InvoiceDTO
    {
        public string SessionId { get; set; }

        public string Currency { get; set; }

        public ulong Amount { get; set; }
    }
}
