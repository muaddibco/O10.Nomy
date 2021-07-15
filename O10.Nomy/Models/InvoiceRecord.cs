using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("InvoiceRecords")]
    public class InvoiceRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InvoiceRecordId { get; set; }

        public NomyUser User { get; set; }

        public string Commitment { get; set; }

        public string RangeProof { get; set; }

        public DateTime? DateOfProcessing { get; set; }
    }
}
