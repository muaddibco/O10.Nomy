using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace O10.Nomy.Models
{
    [Table("SecretInvoiceRecords")]
    public class SecretInvoiceRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SecretInvoiceRecordId { get; set; }

        public NomyUser User { get; set; }

        public InvoiceRecord InvoiceRecord { get; set; }

        public string BlindingFactor { get; set; }
        public ulong Amount { get; set; }
    }
}
