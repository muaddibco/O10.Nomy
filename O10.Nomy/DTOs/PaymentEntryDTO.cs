using O10.Core.Cryptography;

namespace O10.Nomy.DTOs
{
    public class PaymentEntryDTO
    {
        public string SessionId { get; set; }
        public string Commitment { get; set; }
        public string Mask { get; set; }
        public RangeProof RangeProof { get; set; }
    }
}
