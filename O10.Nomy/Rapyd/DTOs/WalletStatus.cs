using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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
