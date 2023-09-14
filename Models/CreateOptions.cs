using System.Text.Json.Serialization;

public class CreateOptions
{
    [JsonPropertyName("submissionId")]
    public string SubmissionId { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("isCorrect")]
    public bool IsCorrect { get; set; }

    [JsonPropertyName("isRequired")]
    public bool IsRequired { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; }
}