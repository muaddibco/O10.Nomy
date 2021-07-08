using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd.DTOs
{
    public class CustomerDTO
    {
        /// <summary>
        /// ID of the Customer object. String starting with cus_
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Address object. See <see cref="AddressDTO"/>
        /// </summary>
        public AddressDTO Address { get; set; }
        /// <summary>
        /// Describes the addresses associated with this customer.
        /// For more information, see <see cref="AddressDTO"/>
        /// </summary>
        public List<AddressDTO> Addresses { get; set; }
        /// <summary>
        /// The tax ID number of the customer. Relevant when the customer is a business
        /// </summary>
        [JsonProperty("business_vat_id")]
        public string BusinessVatId { get; set; }
        /// <summary>
        /// Category of payment method. Response only.
        /// </summary>
        public PaymentMethodCategory Category { get; set; }
        /// <summary>
        /// The ID of a discount coupon that is assigned to this customer. The coupon must use the same currency as the customer's default payment method.
        /// For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference/customer-object#coupon-object">Coupon Object</see>
        /// </summary>
        public string Coupon { get; set; }
        /// <summary>
        /// Time of creation of this customer, in <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/glossary">Unix time</see>. Response only.
        /// </summary>
        [JsonProperty("created_at", ItemConverterType = typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// ID of the customer, a string starting with cus_
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// The payment method that is used when the 'payment' object or subscription does not specify a payment method. Must appear in the payment_methods list. The payment method is referenced by its name field.
        /// </summary>
        [JsonProperty("default_payment_method")]
        public string DefaultPaymentMethod { get; set; }
        /// <summary>
        /// Indicates whether there is currently a failure of an automatic payment that is part of a subscription, or an invoice that was not paid when due.
        /// true: The account is delinquent.
        /// false: The account is current.
        /// Response only.
        /// </summary>
        public bool Delinquent { get; set; }
        /// <summary>
        /// A text description of the customer.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Contains information about the coupon that applies to the customer. Read-only field.
        /// 
        /// Adding a discount is a 2-step process:
        /// 1. <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/create-coupon">Create Coupon</see>, which returns a coupon ID.
        /// 2. Add the coupon ID to the coupon field of the customer with <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/create-customer">Create Customer</see> or <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/update-customer">Update Customer</see>
        /// For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/discount-object">Discount Object</see>
        /// </summary>
        public object Discount { get; set; }
        /// <summary>
        /// Customer's email address. Maximum 512 characters.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// ID of the wallet that is linked to the customer. String starting with ewallet_.
        /// 
        /// Each wallet can be associated with only one customer.
        /// </summary>
        public string Ewallet { get; set; }
        /// <summary>
        /// Contains the fields that are required for the specific payment method.
        /// 
        /// To determine what fields are required for the payment method, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/retrieve-payment-method-required-fields">Get Payment Method Required Fields</see>
        /// </summary>
        public object Fields { get; set; }
        /// <summary>
        /// A custom string that is prefixed to all invoices for this customer. For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/invoice-object">Invoice Object</see>
        /// </summary>
        [JsonProperty("invoice_prefix")]
        public string InvoicePrefix { get; set; }
        /// <summary>
        /// A JSON object defined by the client.
        /// </summary>
        public object Metadata { get; set; }
        /// <summary>
        /// The name of the customer.
        /// Business - The company name.
        /// Individual - The person's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// You cannot delete a payment method that is the designated default payment method.
        /// You can include a payment method when you create the customer using <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/create-customer">Create Customer</see>, or you can add it later with <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/add-payment-method-to-customer">Add Payment Method to Customer</see>.
        /// </summary>
        [JsonProperty("payment_methods")]
        public PaymentMethodsInfoDTO PaymentMethods { get; set; }
        /// <summary>
        /// Customer's primary phone number in E.164 format. The merchant is responsible for verifying that the number is correct. One method of verifying could be to send an activation code to the phone by SMS, with a limited time for response.
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// To add a subscription to a customer, use <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/create-subscription">Create Subscription</see>. For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/subscription-object">Subscription Object</see>
        /// </summary>
        public SubscriptionsInfoDTO Subscriptions { get; set; }
        /// <summary>
        /// ID of the token that represents the card. Relevant to cards.
        /// For more information, see <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/token-object-for-card">Token Object for Card</see>
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Name of the payment method type. For example, us_mastercard_card.
        /// To get a list of payment methods for a country, use <see href="https://docs.rapyd.net/build-with-rapyd/reference-link/list-payment-methods-by-country">List Payment Methods by Country</see>
        /// </summary>
        public string Type { get; set; }
    }
}
