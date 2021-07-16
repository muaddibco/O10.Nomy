using Newtonsoft.Json;
using O10.Nomy.Utils;
using System.Collections.Generic;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class PaymentMethodsInfoDTO
    {
        /// <summary>
        /// A list of up to three payment methods. See <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/payment-method-object">Payment Method Object</see>
        /// </summary>
        public List<object> Data { get; set; }
        /// <summary>
        /// Indicates whether there are more than three payment methods for this customer
        /// </summary>
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        /// <summary>
        /// Total number of payment methods for this customer
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        /// <summary>
        /// URL for requesting all of the payment methods for this customer
        /// </summary>
        public string Url { get; set; }
    }
}
