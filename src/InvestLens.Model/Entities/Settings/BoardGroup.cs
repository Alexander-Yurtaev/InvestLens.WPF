using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class BoardGroup : BaseEntity
{
    [JsonPropertyName("trade_engine_id")]
    public int TradeEngineId { get; set; }

    public virtual Engine? Engine { get; set; }

    [JsonPropertyName("trade_engine_name")]
    public string TradeEngineName { get; set; } = string.Empty;

    [JsonPropertyName("trade_engine_title")]
    public string TradeEngineTitle { get; set; } = string.Empty;

    [JsonPropertyName("market_id")]
    public int MarketId { get; set; }

    public virtual Market? Market { get; set; }

    [JsonPropertyName("market_name")]
    public string MarketName { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("is_default")]
    public bool IsDefault { get; set; }

    [JsonPropertyName("board_group_id")]
    public int board_group_id { get; set; }

    public virtual BoardGroup? BoardGroupEntity { get; set; }

    [JsonPropertyName("is_traded")]
    public bool IsTraded { get; set; }

    [JsonPropertyName("is_order_driven")]
    public bool IsOrderDriven { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}