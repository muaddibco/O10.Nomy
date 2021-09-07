using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("NomyUsers")]
    public class NomyUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NomyUserId { get; set; }

        public NomyAccount Account { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WalletId { get; set; }

        public string? BeneficiaryId { get; set; }

        public string? SenderId { get; set; }

        public long? AdversaryFrom { get; set; }
    }
}
