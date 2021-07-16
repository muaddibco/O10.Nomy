using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum ContactType
    {
        [EnumMember(Value = "personal")]
        Personal,
        [EnumMember(Value = "business")]
        Business
    }
}
