using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs.Beneficiary
{
    [JsonConverter(typeof(RapydConverter))]
    public class BeneficiaryDTO : PropertiesBaseDTO
    {
        public string? Id { get; set; }

        public BeneficiaryCategory Category { get; set; }
        
        [JsonProperty("company_name")]
        public string? CompanyName { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        [JsonProperty("default_payout_method_type")]
        public string DefaultPayoutMethodType { get; set; }

        [JsonProperty("entity_type")]
        public EntityType EntityType { get; set; }
        
        [JsonProperty("first_name")]
        public string? FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        [JsonProperty("identification_type")]
        public IdentificationType IdentificationType { get; set; }

        [JsonProperty("identification_value")]
        public string IdentificationValue { get; set; }

        [JsonProperty("merchant_reference_id")]
        public string MerchantReferenceId { get; set; }

        [JsonProperty("payout_method_type")]
        public string PayoutMethodType { get; set; }

        [JsonProperty("sender_country ")]
        public string SenderCountry { get; set; }

        [JsonProperty("sender_currency")]
        public string SenderCurrency { get; set; }

        [JsonProperty("sender_entity_type")]
        public EntityType SenderEntityType { get; set; }

        public bool Validated { get; set; }

    }
}
