using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("ExpertiseSubAreas")]
    public class ExpertiseSubArea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpertiseSubAreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExpertiseArea ExpertiseArea { get; set; }
    }
}
