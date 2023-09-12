using System.Text.Json.Serialization;

public class CreateQuiz
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("openingDateTime")]
    public DateTime OpeningDateTime { get; set; }

    [JsonPropertyName("closingDateTime")]
    public DateTime ClosingDateTime { get; set; }

    [JsonPropertyName("isPublic")]
    public bool IsPublic { get; set; }

    [JsonPropertyName("tags")]
    public string[] Tags { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}