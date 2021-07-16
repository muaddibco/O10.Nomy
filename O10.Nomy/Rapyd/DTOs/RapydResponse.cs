using Newtonsoft.Json;
using O10.Nomy.Utils;

namespace O10.Nomy.Rapyd.DTOs
{
    [JsonConverter(typeof(RapydConverter))]
    public class RapydResponse<T>
    {
        public ResponseStatusDTO Status { get; set; }
        public T? Data { get; set; }
    }
}
