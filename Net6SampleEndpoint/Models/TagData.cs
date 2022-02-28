using System.Text.Json.Serialization;

namespace Net6SampleEndpoint.Models;

public class TagData
{
    [JsonPropertyName("tag_id")]
    public string? TagId { get; set; }

    [JsonPropertyName("gateway_id")]
    public string? GatewayId { get; set; }
}