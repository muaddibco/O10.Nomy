using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum WalletCategory
    {
        [EnumMember(Value = "collect")]
        Collect,
        [EnumMember(Value = "disburse")]
        Disburse,
        [EnumMember(Value = "card_authorization")]
        CardAuthorization,
        [EnumMember(Value = "general")]
        General
    }
}
