using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("NomyAccount")]
    public class NomyAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NomyAccountId { get; set; }

        public long O10Id { get; set; }
    }
}
