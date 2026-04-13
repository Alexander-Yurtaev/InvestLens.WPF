using InvestLens.Model.Enums;

namespace InvestLens.Model.Entities;

public class Portfolio
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public PortfolioType PortfolioType { get; set; }
    public required int OwnerId { get; set; }
    public User? Owner { get; set; }
}