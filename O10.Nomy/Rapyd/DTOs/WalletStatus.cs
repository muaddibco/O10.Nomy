using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum WalletStatus
    {
        [EnumMember(Value = "ACT")]
        Active,
        [EnumMember(Value = "DIS")]
        Disabled
    }
}
