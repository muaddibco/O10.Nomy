using Newtonsoft.Json;
using O10.Nomy.Utils;
using System;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class AccountDTO
    {
        /// <summary>
        /// ID of the account. UUID.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Three-letter ISO 4217 code for the currency of the account.
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Available funds in the account.
        /// </summary>
        public string Balance { get; set; }
        /// <summary>
        /// Three-letter ISO 4217 code for the currency used in the balance field. Uppercase.
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Reserved
        /// </summary>
        public string Limit { get; set; }
        /// <summary>
        /// Reserved
        /// </summary>
        public object Limits { get; set; }
        /// <summary>
        ///  Amount in the on-hold balance of the account.
        /// </summary>
        [JsonProperty("on_hold_balance")]
        public string OnHoldBalance { get; set; }
        /// <summary>
        /// Amount of escrow funds in the account.
        /// </summary>
        [JsonProperty("received_balance")]
        public string ReceivedBalance { get; set; }
        /// <summary>
        /// Amount in the reserve balance of the account.
        /// </summary>
        [JsonProperty("reserve_balance")]
        public string ReserveBalance { get; set; }
    }
}
