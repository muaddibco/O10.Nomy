using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("NomyServiceProvider")]
    public class NomyServiceProvider
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NomyServiceProviderId { get; set; }

        public NomyAccount Account { get; set; }

        public string Name { get; set; }
    }
}
