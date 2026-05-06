using InvestLens.Model.Entities.Settings;

namespace InvestLens.Model;

public class BoardModel : BaseModel
{
    public int BoardGroupId { get; set; }
    public int EngineId { get; set; }
    public virtual Engine? Engine { get; set; }
    public int MarketId { get; set; }
    public string? BoardId { get; set; }
    public string BoardTitle { get; set; } = string.Empty;
    public bool IsTraded { get; set; }
    public bool HasCandles { get; set; }
    public bool IsPrimary { get; set; }
}