using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Moex;

public class History : BaseEntity
{
    [JsonPropertyName("SECID")]
    public string SecId { get; set; } = string.Empty;

    [JsonPropertyName("CLOSE")]
    public decimal Close { get; set; }

    [JsonPropertyName("CURRENCYID")]
    public string CurrenencyId { get; set; } = string.Empty;

    [JsonPropertyName("TRADEDATE")]
    public DateTime Date { get; set; }
}