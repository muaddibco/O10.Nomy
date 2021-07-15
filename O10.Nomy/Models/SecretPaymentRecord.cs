using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("SecretPaymentRecords")]
    public class SecretPaymentRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SecretPaymentRecordId { get; set; }

        public NomyUser User { get; set; }

        public PaymentRecord PaymentRecord { get; set; }

        public string BlindingFactor { get; set; }
        public ulong Amount { get; set; }
    }
}
