using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class Duration
{
    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("duration")]
    public int DurationCount { get; set; }

    [JsonPropertyName("days")]
    public int? days { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;
}