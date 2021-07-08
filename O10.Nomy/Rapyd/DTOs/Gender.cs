using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum Gender
    {
        [EnumMember(Value ="male")]
        Male,
        [EnumMember(Value = "female")]
        Female,
        [EnumMember(Value = "other")]
        Other,
        [EnumMember(Value = "not_applicable")]
        NotApplicable
    }
}
