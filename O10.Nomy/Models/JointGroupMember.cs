using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("JointGroupMembers")]
    public class JointGroupMember
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long JointGroupMemberId { get; set; }

        public JointGroup Group { get; set; }

        public string Email { get; set; }

        public bool IsRegistered { get; set; }
    }
}
