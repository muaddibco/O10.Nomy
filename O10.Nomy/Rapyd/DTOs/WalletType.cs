using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum WalletType
    {
        [EnumMember(Value = "company")]
        Company,
        [EnumMember(Value = "person")]
        Person,
        [EnumMember(Value = "client")]
        Client
    }
}
