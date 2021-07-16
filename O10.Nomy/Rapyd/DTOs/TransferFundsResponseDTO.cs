using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class TransferFundsResponseDTO
    {
        public string Id { get; set; }
        public TransferFundsStatus Status { get; set; }
        public ulong Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string? DestinationPhoneNumber{ get; set; }
        public string DestinationWalletId { get; set; }
        public string DestinationTransactionId { get; set; }
        public string SourceWalletId { get; set; }
        public string SourceTransactionId { get; set; }
    }
}
