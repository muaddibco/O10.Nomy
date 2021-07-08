using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum PaymentMethodCategory
    {
        [EnumMember(Value = "bank_redirect")]
        BankRedirect,
        [EnumMember(Value = "bank_transfer")]
        BankTransfer,
        [EnumMember(Value = "card")]
        Card,
        [EnumMember(Value = "cash")]
        Cash,
        [EnumMember(Value = "ewallet")]
        Ewallet,
        [EnumMember(Value = "rapyd_ewallet")]
        RapydEwallet
    }
}
