using System.Collections.Generic;

namespace O10.Nomy.DTOs
{
    public class ExpertProfileDTO
    {
        public long UserId { get; set; }
        public long ExpertProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public ulong Fee { get; set; }
        public string Email { get; set; }
        public List<string> ExpertiseSubAreas { get; set; }
        public string WalletId { get; set; }
    }
}
