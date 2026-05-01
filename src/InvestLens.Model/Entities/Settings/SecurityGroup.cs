using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class SecurityGroup : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }
}