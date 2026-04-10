namespace InvestLens.Model.Portfolio;

public class LookupModel(string title)
{
    public string Title { get; set; } = title;
    public string Description { get; set; } = string.Empty;
    public bool IsChecked { get; set; }
}