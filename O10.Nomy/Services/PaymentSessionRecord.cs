using O10.Core.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace O10.Nomy.Services
{
    public class PaymentSession
    {
        public PaymentSession()
        {
            Records = new();
        }

        public string SessionId { get; set; }

        public Dictionary<string, PaymentSessionRecord> Records { get; set; }
    }

    public class PaymentSessionRecord
    {
        public PaymentSessionRecord()
        {
            TimeoutCancellation = new CancellationTokenSource();

            TimeoutTask = new Task<PaymentSessionRecord>(() => 
            {
                Task.Delay(5000).Wait();
                return this;
            }, TimeoutCancellation.Token);
        }

        public string SessionId { get; set; }
        public PaymentSessionEntry InvoiceEntry { get; set; }
        public PaymentSessionEntry PaymentEntry { get; set; }
        public BorromeanRingSignature PaymentSignature { get; set; }

        public Task<PaymentSessionRecord> TimeoutTask { get; set; }

        public CancellationTokenSource TimeoutCancellation { get; set; }
    }

    public class PaymentSessionEntry
    {
        public byte[] Commitment { get; set; }

        public byte[] Mask { get; set; }

        public RangeProof RangeProof { get; set; }
    }
}
