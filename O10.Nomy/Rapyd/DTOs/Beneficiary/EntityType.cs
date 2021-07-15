using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs.Beneficiary
{
    public enum EntityType
    {
        [EnumMember(Value = "individual")]
        Individual,
        [EnumMember(Value = "company")]
        Company
    }
}
