using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using O10.Nomy.Utils;
using System;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class WalletContact
    {
        /// <summary>
        /// ID of the Contact object. String starting with cont_
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Address of this contact person. Required when the client issues a card to the contact. 
        /// This is the ID of an address object created with <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/create-address">Create Address</see>.For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference/wallet-contact-object#address-object">Address Object</see>
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Describes additional information for business entities. See Business Details Object, below. 
        /// Required when type is 'company'.
        /// </summary>
        [JsonProperty("business_details")]
        public object BusinessDetails { get; set; }

        /// <summary>
        /// Indicates the degree to which this contact can use the wallet. One of the following:
        /// * 1 - All transactions are allowed.
        /// * 0 - The wallet is limited, and the contact must complete the identity verification process.
        /// * -1 - The wallet is restricted and cannot be used for any transactions.
        /// Response only.
        /// </summary>
        [JsonProperty("compliance_profile")]
        public int ComplianceProfile { get; set; }

        /// <summary>
        /// Type of contact. One of the following:
        /// personal - An individual customer.
        /// business - A business customer.
        /// </summary>
        [JsonProperty("contact_type")]
        public ContactType ContactType { get; set; }

        /// <summary>
        /// The two-letter ISO 3166-1 ALPHA-2 code for the country. Uppercase.
        /// To determine the code for a country, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/list-countries">List Countries</see>
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Time of creation of the Contact object, in <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/glossary">Unix time</see>. Response only.
        /// </summary>
        [JsonProperty("created_at", ItemConverterType = typeof(UnixDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Date of birth of the individual. Format: MM/DD/YYYY
        /// </summary>
        [JsonProperty("date_of_birth")]
        [JsonConverter(typeof(DateDateTimeConverter))]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email address of the contact.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// ID of the Rapyd Wallet that this contact is associated with. String starting with ewallet_.
        /// </summary>
        public string Ewallet { get; set; }

        /// <summary>
        /// First name of the personal contact or primary person associated with the business contact.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gender of the personal contact or primary person associated with the business contact
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Description of the type of residency at the contact's residence
        /// </summary>
        public HouseType HouseType { get; set; }

        /// <summary>
        /// ID number as shown by the ID document.
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        /// Type of the identification document associated with the contact. Must be uppercase.
        /// For types that are valid in the country, use <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/list-official-identification-documents">List Official Identification Documents</see>
        /// </summary>
        public string IdentificationType { get; set; }

        /// <summary>
        /// Family name of the personal contact or primary person associated with the business contact. This field is required to issue a card to a personal contact.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Marital status of the personal contact or primary person associated with the business contact.
        /// </summary>
        [JsonProperty("marital_status")]
        public MaritalStatus MaritalStatus { get; set; }

        /// <summary>
        /// A JSON object defined by the client
        /// </summary>
        public object Metadata { get; set; }

        /// <summary>
        /// Middle name of the personal contact or primary person associated with the business contact.
        /// </summary>
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Name of the mother of the personal contact or primary person associated with the business contact. Alphabetic characters and spaces.
        /// </summary>
        [JsonProperty("mothers_name")]
        public string MothersName { get; set; }

        /// <summary>
        /// The citizenship of the contact. Two-letter ISO 3166-1 ALPHA-2 code for the country. Uppercase.
        /// To determine the code for a country, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/list-countries">List Countries</see>
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// Phone number of the contact in E.164 format.
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Second last name of the personal contact or primary person associated with the business contact.
        /// </summary>
        [JsonProperty("second_last_name")]
        public string SecondLastName { get; set; }

        /// <summary>
        /// Determines whether Rapyd sends notifications to the contact. Default is 'false'.
        /// </summary>
        [JsonProperty("send_notifications")]
        public bool SendNotifications { get; set; }

        /// <summary>
        /// Result of the verification check. One of the following:
        /// not verified - The contact has not been submitted for the Know Your Customer checks.
        /// KYCd - The user has passed the Know Your Customer checks.
        /// </summary>
        [JsonProperty("verification_status")]
        public VerificationStatus VerificationStatus { get; set; }
    }
}
