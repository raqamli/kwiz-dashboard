using System.Text.Json.Serialization;
namespace Kwiz.Dashboard.Models;
public class CreateQuestion
{
    [JsonPropertyName("timeLimitSeconds")]
    public int TimeLimitSeconds { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }
}