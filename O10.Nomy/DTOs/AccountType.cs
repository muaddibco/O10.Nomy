using System.ComponentModel;

namespace O10.Nomy.DTOs
{
    public enum AccountType : byte
    {
        [Description("Identity Provider")]
        IdentityProvider = 1,

        [Description("Service Provider")]
        ServiceProvider = 2,

        [Description("Regular User")]
        User = 3
    }
}