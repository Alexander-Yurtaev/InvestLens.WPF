using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class SecurityType : BaseEntity
{
    [JsonPropertyName("trade_engine_id")]
    public int TradeEngineId { get; set; }

    public virtual Engine? TradeEngine { get; set; }

    [JsonPropertyName("trade_engine_name")]
    public string trade_engine_name { get; set; } = string.Empty;

    [JsonPropertyName("trade_engine_title")]
    public string TradeEngineTitle { get; set; } = string.Empty;

    [JsonPropertyName("security_type_name")]
    public string SecurityTypeName { get; set; } = string.Empty;

    [JsonPropertyName("security_type_title")]
    public string SecurityTypeTitle { get; set; } = string.Empty;

    [JsonPropertyName("security_group_name")]
    public string SecurityGroupName { get; set; } = string.Empty;

    [JsonPropertyName("stock_type")]
    public string StockType { get; set; } = string.Empty;
}