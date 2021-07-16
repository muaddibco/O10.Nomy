using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class SetTransferDTO
    {
        public string Id { get; set; }
        public SetTransferStatus Status { get; set; }
    }
}
