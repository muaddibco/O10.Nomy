using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("JointGroups")]
    public class JointGroup
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long JointGroupId { get; set; }
        public long O10RegistrationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Collection<JointGroupMember> Members { get; set; }
    }
}
