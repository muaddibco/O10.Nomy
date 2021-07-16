using Newtonsoft.Json;
using O10.Nomy.Utils;
using System.Collections.Generic;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class WalletRequestDTO
    {
        /// <summary>
        /// Unique identifier of the wallet object. String starting with ewallet_
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// An array of accounts.
        /// Response only.
        /// </summary>
        public List<AccountDTO> Accounts { get; set; }

        /// <summary>
        /// Indicates the type of client wallet
        /// Relevant to client wallets.Read-only
        /// </summary>
        public WalletCategory Category { get; set; }

        /// <summary>
        ///  Describes details about the wallet contact. A 'person' wallet can have only one contact
        /// </summary>
        public WalletContact? Contact { get; set; }

        /// <summary>
        /// Describes details about the wallet contacts. See <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/wallet-contact-object">Wallet Contact Object</see>. A 'person' wallet can have only one contact
        /// </summary>
        public List<WalletContact>? Contacts { get; set; }

        /// <summary>
        /// Email address of the wallet owner
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Wallet ID defined by the customer or end user. Must be unique
        /// </summary>
        public string? EwalletReferenceId { get; set; }

        /// <summary>
        /// First name of the wallet owner
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Family name of the wallet owner
        /// </summary>
        [JsonProperty("last_name")]
        public string? LastName { get; set; }

        /// <summary>
        /// A JSON object defined by the client
        /// </summary>
        public object? Metadata { get; set; }

        /// <summary>
        /// Phone number of the wallet owner in E.164 format.
        /// </summary>
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Status of the wallet. Response only
        /// </summary>
        public WalletStatus Status { get; set; }

        /// <summary>
        /// Type of wallet. One of the following:
        /// * company - Indicates a business wallet.
        /// * person - Indicates the wallet of an individual consumer.
        /// * client - Indicates a wallet for the Rapyd client.Contact Rapyd support to create this type of wallet.
        /// </summary>
        public WalletType Type { get; set; }

        /// <summary>
        /// Result of the verification check. One of the following:
        /// * not verified - The contact has not been submitted for the Know Your Customer checks.
        /// * KYCd - The user has passed the Know Your Customer checks.
        /// Response only.
        /// </summary>
        [JsonProperty("verification_status")]
        public VerificationStatus VerificationStatus { get; set; }
    }
}
