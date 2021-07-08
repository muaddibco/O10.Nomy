using Newtonsoft.Json;

namespace O10.Nomy.Rapyd.DTOs
{
    public class DepositFundsResponseDTO
    {
        public string Id { get; set; }

        [JsonProperty("accout_id")]
        public string AccountId { get; set; }

        public int Amount { get; set; }
        public string Currency { get; set; }

        [JsonProperty("balance_type")]
        public string BalanceType { get; set; }
    }
}
