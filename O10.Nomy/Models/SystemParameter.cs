using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("SystemParameters")]
    public class SystemParameter
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SystemParameterId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
