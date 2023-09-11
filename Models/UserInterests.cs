using System.Text.Json.Serialization;

namespace Kwiz.Dashboard.Models;
public class UserInterests
{
    [JsonPropertyName("interestedTechnologies")]
    public List<Technology> InterestedTechnologies { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}