using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum SetTransferStatus
    {
        [EnumMember(Value = "accept")]
        Accept,
        [EnumMember(Value = "decline")]
        Decline,
        [EnumMember(Value = "cancel")]
        Cancel
    }
}
