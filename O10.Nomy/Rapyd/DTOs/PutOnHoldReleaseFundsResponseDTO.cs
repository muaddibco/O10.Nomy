using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class PutOnHoldReleaseFundsResponseDTO
    {
        public string Id { get; set; }

        [JsonProperty("source_transaction_id")]
        public string SourceTransactionId { get; set; }

        [JsonProperty("destination_transaction_id")]
        public string DestinationTransactionId { get; set; }

        [JsonProperty("source_user_profile_id")]
        public string SourceUserProfileId { get; set; }

        [JsonProperty("destination_user_profile_id")]
        public string DestinationUserProfileId { get; set; }

        [JsonProperty("source_account_id")]
        public string SourceAccountId { get; set; }

        [JsonProperty("destination_account_id")]
        public string DestinationAccountId { get; set; }

        [JsonProperty("source_balance_type")]
        public string SourceBalanceType { get; set; }

        [JsonProperty("destination_balance_type")]
        public string DestinationBalanceType { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        public ulong Amount { get; set; }
    }
}
