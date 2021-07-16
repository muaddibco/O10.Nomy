using Newtonsoft.Json;
using O10.Nomy.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd.DTOs.Disburse
{
    [JsonConverter(typeof(RapydConverter))]
    public class PayoutRequestDTO
    {
        public string Ewallet { get; set; }
        public ulong PayoutAmount { get; set; }
        public string SenderCurrency { get; set; }
        public string SenderCountry { get; set; }
        public string BeneficiaryCountry { get; set; }
        public string PayoutCurrency { get; set; }
        public EntityType SenderEntityTypy { get; set; }
        public EntityType BeneficiaryEntityType { get; set; }
        public string Beneficiary { get; set; }
    }
}
