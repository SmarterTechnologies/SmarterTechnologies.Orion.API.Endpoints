using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Net5SampleEndpoint.Models
{
    public class TagData
    {
        [JsonPropertyName("tag_id")]
        public string TagId { get; set; }

        [JsonPropertyName("gateway_id")]
        public string GatewayId { get; set; }
    }
}
