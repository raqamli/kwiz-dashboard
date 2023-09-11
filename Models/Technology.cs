
using System.Text.Json.Serialization;

namespace Kwiz.Dashboard.Models;

public class Technology
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; } 
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }
}