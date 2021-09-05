using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.DemoFeatures.Models
{
    [Table(nameof(DemoValidation))]
    public class DemoValidation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DemoValidationId { get; set; }

        public string Action { get; set; }

        public string StaticData { get; set; }

        public string DynamicData { get; set; }
    }
}
