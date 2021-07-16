using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs.Disburse
{
    [JsonConverter(typeof(RapydConverter))]
    public class BeneficiaryDTO : PropertiesBaseDTO
    {
        public string? Id { get; set; }

        public BeneficiaryCategory Category { get; set; }

        public string? CompanyName { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public string DefaultPayoutMethodType { get; set; }

        public EntityType EntityType { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public IdentificationType IdentificationType { get; set; }

        public string IdentificationValue { get; set; }

        public string MerchantReferenceId { get; set; }

        public string PayoutMethodType { get; set; }

        public string SenderCountry { get; set; }

        public string SenderCurrency { get; set; }

        public EntityType SenderEntityType { get; set; }

        public bool Validated { get; set; }
    }
}
