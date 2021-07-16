using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs.Disburse
{
    public enum BeneficiaryCategory
    {
        [EnumMember(Value = "bank")]
        Bank,
        [EnumMember(Value = "card")]
        Card,
        [EnumMember(Value = "cash")]
        Cash,
        [EnumMember(Value = "ewallet")]
        EWallet,
        [EnumMember(Value = "rapyd_ewallet")]
        RapydEWallet
    }
}
