namespace InvestLens.Model.MoexApi;

public class HistoryModel : BaseModel
{
    public string SecId { get; set; } = string.Empty;
    public decimal Close { get; set; }
    public string CurrenencyId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}