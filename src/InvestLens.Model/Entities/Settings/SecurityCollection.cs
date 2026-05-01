using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class SecurityCollection : BaseEntity
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("security_group_id")]
    public int SecurityGroupId { get; set; }

    public virtual SecurityGroup? SecurityGroup { get; set; }
}