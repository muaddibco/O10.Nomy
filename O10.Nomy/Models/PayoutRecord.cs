using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace O10.Nomy.Models
{
    [Table("PayoutRecords")]
    public class PayoutRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PayoutRecordId { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public ulong TotalAmount { get; set; }

        public virtual ICollection<InvoiceRecord> Invoices { get; set; }
        public virtual ICollection<PaymentRecord> Payments { get; set; }
    }
}
