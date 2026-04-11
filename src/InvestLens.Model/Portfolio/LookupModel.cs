namespace InvestLens.Model.Portfolio;

public class LookupModel(string title)
{
    public string Name { get; set; } = title;
    public string Description { get; set; } = string.Empty;
}