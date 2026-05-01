using System.Text.Json.Serialization;

namespace InvestLens.Model.Entities.Settings;

public class Market : BaseEntity
{
    [JsonPropertyName("trade_engine_id")]
    public int TradeEngineId { get; set; }

    public virtual Engine? TradeEngine { get; set; }

    [JsonPropertyName("trade_engine_name")]
    public string TradeEngineName { get; set; } = string.Empty;

    [JsonPropertyName("trade_engine_title")]
    public string TradeEngineTitle { get; set; } = string.Empty;

    [JsonPropertyName("market_name")]
    public string MarketName { get; set; } = string.Empty;

    [JsonPropertyName("market_title")]
    public string MarketTitle { get; set; } = string.Empty;

    [JsonPropertyName("market_id")]
    public int MarketId { get; set; }

    public virtual Market? MarketEntity { get; set; }

    [JsonPropertyName("marketplace")]
    public string MarketPlace { get; set; } = string.Empty;

    [JsonPropertyName("is_otc")]
    public bool IsOtc { get; set; }

    [JsonPropertyName("has_history_files")]
    public bool HasHistoryFiles { get; set; }

    [JsonPropertyName("has_history_trades_files")]
    public bool HasHistoryTradesFiles { get; set; }

    [JsonPropertyName("has_trades")]
    public bool HasTrades { get; set; }

    [JsonPropertyName("has_history")]
    public bool HasHistory { get; set; }

    [JsonPropertyName("has_candles")]
    public bool HasCandles { get; set; }

    [JsonPropertyName("has_orderbook")]
    public bool HasOrderbook { get; set; }

    [JsonPropertyName("has_tradingsession")]
    public bool HasTradingsession { get; set; }

    [JsonPropertyName("has_extra_yields")]
    public bool HasExtraYields { get; set; }

    [JsonPropertyName("has_delay")]
    public bool HasDelay { get; set; }
}