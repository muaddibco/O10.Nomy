using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd.DTOs
{
    public class PutFundsOnHoldDTO
    {
        [JsonProperty("ewallet")]
        public string WalletId { get; set; }

        public ulong Amount { get; set; }

        public string Currency { get; set; }
    }
}
