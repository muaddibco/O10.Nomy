using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using O10.Nomy.Utils;
using System;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class AddressDTO
    {
        /// <summary>
        /// ID of the Address object. String starting with address_
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the canton administrative subdivision, as used in banking.
        /// </summary>
        public string Canton { get; set; }

        /// <summary>
        /// City portion of the address. Required for issuance of a card to the wallet contact.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The two-letter ISO 3166-1 ALPHA-2 code for the country. Uppercase.
        /// To determine the code for a country, <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/retrieve-all-countries">see</see>.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Time of creation of the 'address' object, in <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/glossary">Unix time</see>. Response only.
        /// </summary>
        [JsonProperty("created_at", ItemConverterType = typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Name of the district administrative subdivision, as used in banking.
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// Line 1 of the address, such as a building number and street name.
        /// </summary>
        public string Line1 { get; set; }
        /// <summary>
        /// Line 2 of the address, such as a suite or apartment number, or the name of a named building.
        /// </summary>
        public string Line2 { get; set; }
        /// <summary>
        /// Line 3 of the address.
        /// </summary>
        public string Line3 { get; set; }
        /// <summary>
        /// A JSON object defined by the client.
        /// </summary>
        public object Metadata { get; set; }
        /// <summary>
        /// The name of a contact person or an "in care of" person at this address.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Phone number associated with this specific address in E.164 format. Must be unique.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// State or province portion of the address.
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Time of the last update to the Address object, in <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/glossary">Unix time</see>. Response only.
        /// </summary>
        [JsonProperty("updated_at", ItemConverterType = typeof(UnixDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Postal code portion of the address.
        /// </summary>
        public string Zip { get; set; }
    }
}
