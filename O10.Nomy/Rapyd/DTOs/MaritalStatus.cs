using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum MaritalStatus
    {
        [EnumMember(Value = "married")]
        Married,
        [EnumMember(Value = "single")]
        Single,
        [EnumMember(Value = "divorced")]
        Divorced,
        [EnumMember(Value = "widowed")]
        Widowed,
        [EnumMember(Value = "cohabiting")]
        Cohabiting,
        [EnumMember(Value = "not_applicable")]
        NotApplicable
    }
}
