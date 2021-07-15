using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace O10.Nomy.Rapyd.DTOs.Beneficiary
{
    public enum BeneficiaryCategory
    {
        [EnumMember(Value ="bank")]
        Bank,
        [EnumMember(Value ="card")]
        Card,
        [EnumMember(Value ="cash")]
        Cash,
        [EnumMember(Value ="ewallet")]
        EWallet,
        [EnumMember(Value = "rapyd_ewallet")]
        RapydEWallet
    }
}
