using Newtonsoft.Json;

namespace O10.Nomy.Rapyd.DTOs
{
    public class DepositFundsRequestDTO
    {
        [JsonProperty("ewallet")]
        public string WalletId { get; set; }

        public ulong Amount { get; set; }

        public string Currency { get; set; }
    }
}
