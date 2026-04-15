using InvestLens.Model.Enums;

namespace InvestLens.Model.Entities;

public class Portfolio
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public PortfolioType PortfolioType { get; set; }
    public int? ParentPortfolioId { get; set; }
    public virtual Portfolio? ParentPortfolio { get; set; }
    public virtual ICollection<Portfolio> ChildrenPortfolios { get; set; } = [];
    public required int OwnerId { get; set; }
    public virtual User? Owner { get; set; }
}