using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class Board : BaseEntity
{
    [JsonPropertyName("board_group_id")]
    public int BoardGroupId { get; set; }

    public virtual BoardGroup? BoardGroup { get; set; }

    [JsonPropertyName("engine_id")]
    public int EngineId { get; set; }

    public virtual Engine? Engine { get; set; }

    [JsonPropertyName("market_id")]
    public int MarketId { get; set; }

    public virtual Market? Market { get; set; }

    [JsonPropertyName("boardid")]
    public string? BoardId { get; set; }

    public virtual Board? BoardEntity { get; set; }

    [JsonPropertyName("board_title")]
    public string BoardTitle { get; set; } = string.Empty;

    [JsonPropertyName("is_traded")]
    public bool IsTraded { get; set; }

    [JsonPropertyName("has_candles")]
    public bool HasCandles { get; set; }

    [JsonPropertyName("is_primary")]
    public bool IsPrimary { get; set; }
}