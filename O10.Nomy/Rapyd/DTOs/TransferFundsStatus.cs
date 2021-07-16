using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum TransferFundsStatus
    {
        [EnumMember(Value = "CAN")]
        Cancelled,
        [EnumMember(Value = "DEC")]
        Declined,
        [EnumMember(Value = "PEN")]
        Pending,
        [EnumMember(Value = "CLO")]
        Closed
    }
}
