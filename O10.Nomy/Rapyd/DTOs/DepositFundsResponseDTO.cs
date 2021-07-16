using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class DepositFundsResponseDTO
    {
        public string Id { get; set; }

        [JsonProperty("accout_id")]
        public string AccountId { get; set; }

        public ulong Amount { get; set; }
        public string Currency { get; set; }

        [JsonProperty("balance_type")]
        public string BalanceType { get; set; }
    }
}
