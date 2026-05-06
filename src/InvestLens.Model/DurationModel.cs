namespace InvestLens.Model;

public class DurationModel : BaseModel
{
    public int Interval { get; set; }
    public int DurationCount { get; set; }
    public int? Days { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
}