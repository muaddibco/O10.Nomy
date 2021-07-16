using System.Runtime.Serialization;

namespace O10.Nomy.Rapyd.DTOs
{
    public enum IdentificationType
    {
        [EnumMember(Value = "company_registered_number")]
        CompanyRegisteredNumber,
        [EnumMember(Value = "drivers_license")]
        DriversLicense,
        [EnumMember(Value = "identification_id")]
        IdentificationId,
        [EnumMember(Value = "international_passport")]
        InternationalPassport,
        [EnumMember(Value = "residence_permit")]
        ResidencePermit,
        [EnumMember(Value = "social_security")]
        SocialSecurity,
        [EnumMember(Value = "work_permit")]
        WorkPermit
    }
}
