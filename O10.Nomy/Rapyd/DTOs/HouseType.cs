using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum HouseType
    {
        [EnumMember(Value = "lease")]
        Lease,
        [EnumMember(Value = "live_with_family")]
        LiveWithFamily,
        [EnumMember(Value = "own")]
        Own,
        [EnumMember(Value = "owner")]
        Owner,
        [EnumMember(Value = "month_to_month")]
        MonthToMonth,
        [EnumMember(Value = "housing_project")]
        HousingProject
    }
}
