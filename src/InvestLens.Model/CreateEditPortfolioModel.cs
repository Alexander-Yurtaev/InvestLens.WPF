namespace InvestLens.Model;

public class CreateEditPortfolioModel
{
    public string Title { get; set; } = string.Empty;

    public string ActionTitle { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsPortfolioSimpleType { get; set; } = true;
}