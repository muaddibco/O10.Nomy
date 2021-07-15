using Newtonsoft.Json;
using System.Collections.Generic;

namespace O10.Nomy.Rapyd.DTOs
{
    public class PropertiesBaseDTO
    {
        [JsonIgnore]
        public Dictionary<string, string> Properties { get; set; }
    }
}
