using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("ExpertiseAreas")]
    public class ExpertiseArea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpertiseAreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ExpertiseSubArea> ExpertiseSubAreas { get; set; }
    }
}
