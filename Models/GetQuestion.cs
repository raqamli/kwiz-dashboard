using System.Text.Json.Serialization;

public class GetQuestion
{
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("items")]
    public List<Question> Items { get; set; }

    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage { get; set; }

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }
}
public class Question
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("timeLimitSeconds")]
    public int TimeLimitSeconds { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }
}

