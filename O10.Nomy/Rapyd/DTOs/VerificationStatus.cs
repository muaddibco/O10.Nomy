using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum VerificationStatus
    {
        [EnumMember(Value = "not verified")]
        NotVerified,
        [EnumMember(Value = "KYCd")]
        KYCd
    }
}
