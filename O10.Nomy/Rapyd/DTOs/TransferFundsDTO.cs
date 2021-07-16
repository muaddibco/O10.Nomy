using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class TransferFundsDTO
    {
        public string SourceEwallet { get; set; }
        public ulong Amount { get; set; }
        public string DestinationEwallet { get; set; }
        public string Currency { get; set; }
    }
}
