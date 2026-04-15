namespace InvestLens.Model.Portfolio;

public class LookupModel(int id, string title)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = title;
    public string Description { get; set; } = string.Empty;
}