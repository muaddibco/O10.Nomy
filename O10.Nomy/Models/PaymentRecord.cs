using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("PaymentRecords")]
    public class PaymentRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentRecordId { get; set; }

        public NomyUser User { get; set; }

        public InvoiceRecord Invoice { get; set; }

        public string Commitment { get; set; }

        public string RangeProof { get; set; }

        public string Signature { get; set; }

        public DateTime? DateOfProcessing { get; set; }
    }
}
