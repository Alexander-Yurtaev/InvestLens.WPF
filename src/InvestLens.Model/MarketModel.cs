using InvestLens.Model.Entities.Settings;
using System.Text.Json.Serialization;

namespace InvestLens.Model;

public class MarketModel : BaseModel
{
    public int TradeEngineId { get; set; }
    public string TradeEngineName { get; set; } = string.Empty;
    public string TradeEngineTitle { get; set; } = string.Empty;
    public string MarketName { get; set; } = string.Empty;
    public string MarketTitle { get; set; } = string.Empty;
    public int MarketId { get; set; }
    public string MarketPlace { get; set; } = string.Empty;
    public bool IsOtc { get; set; }
    public bool HasHistoryFiles { get; set; }
    public bool HasHistoryTradesFiles { get; set; }
    public bool HasTrades { get; set; }
    public bool HasHistory { get; set; }
    public bool HasCandles { get; set; }
    public bool HasOrderbook { get; set; }
    public bool HasTradingsession { get; set; }
    public bool HasExtraYields { get; set; }
    public bool HasDelay { get; set; }
}