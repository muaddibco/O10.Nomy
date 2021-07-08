using Newtonsoft.Json;
using System.Collections.Generic;

namespace O10.Nomy.Rapyd.DTOs
{
    public class SubscriptionsInfoDTO
    {
        /// <summary>
        /// A list of up to three payment methods. See <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/subscription-object">Subscription Object</see>
        /// </summary>
        public List<object> Data { get; set; }
        /// <summary>
        /// Indicates whether there are more than three subscriptions for this customer
        /// </summary>
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        /// <summary>
        /// Total number of subscriptions for this customer
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        /// <summary>
        /// URL for requesting all of the subscriptions for this customer
        /// </summary>
        public string Url { get; set; }
    }
}
