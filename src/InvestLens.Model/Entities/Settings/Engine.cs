using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class Engine : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}