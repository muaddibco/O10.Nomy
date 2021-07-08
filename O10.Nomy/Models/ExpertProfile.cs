using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("ExpertProfiles")]
    public class ExpertProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpertProfileId { get; set; }

        [Required]
        public NomyUser NomyUser { get; set; }

        public string Description { get; set; }

        public ulong Fee { get; set; }

        public virtual ICollection<ExpertiseSubArea> ExpertiseSubAreas { get; set; }
    }
}
