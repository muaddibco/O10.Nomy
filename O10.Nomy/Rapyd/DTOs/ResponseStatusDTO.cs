using Newtonsoft.Json;
using System;

namespace O10.Nomy.Rapyd.DTOs
{
    public class ResponseStatusDTO
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }

        [JsonProperty("response_code")]
        public string ResponseCode { get; set; }

        [JsonProperty("operation_id")]
        public Guid OperationId { get; set; }
    }
}
