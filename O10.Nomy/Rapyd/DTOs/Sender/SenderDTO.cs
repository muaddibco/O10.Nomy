using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs.Sender
{
    [JsonConverter(typeof(RapydConverter))]
    public class SenderDTO
    {
        public string? Id { get; set; }
        public string? CompanyName { get; set; }

        public string Country { get; set; }

        public string Currency { get; set; }

        public EntityType EntityType { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public IdentificationType IdentificationType { get; set; }

        public string IdentificationValue { get; set; }
    }
}
