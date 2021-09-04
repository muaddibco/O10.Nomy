using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("JointServiceRegistrations")]
    public class JointServiceRegistration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long JointServiceRegistrationId { get; set; }

        public string RegistrationCommitment { get; set; }

        public string Email { get; set; }

        public long O10RegistrationId { get; set; }
    }
}
