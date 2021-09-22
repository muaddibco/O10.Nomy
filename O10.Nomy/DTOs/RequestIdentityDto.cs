using O10.Client.Web.DataContracts.User;

namespace O10.Nomy.DTOs
{
    public class RequestIdentityDto
    {
        public UserAttributeSchemeDto AttributeScheme { get; set; }
        public string Password { get; set; }
    }
}
