using Newtonsoft.Json;
using O10.Nomy.Utils;
using System.Collections.Generic;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class WalletContactsInfo
    {
        /// <summary>
        /// An array of ‘contact’ objects. See <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/wallet-contact">Wallet Contact</see>
        /// </summary>
        public List<object> Data { get; set; }
        /// <summary>
        /// Indicates whether there are more contacts than appear in the ‘data’ array
        /// </summary>
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        /// <summary>
        ///  The total number of contacts in the wallet
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        /// <summary>
        /// URL to retrieve all contacts in the wallet. Use HTTP GET
        /// </summary>
        public string Url { get; set; }
    }
}
