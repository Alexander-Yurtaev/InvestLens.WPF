using InvestLens.Model.Enums;
using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model.Entities;

public class Portfolio : BaseEntity
{
    [SetsRequiredMembers]
    public Portfolio(string name, PortfolioType portfolioType, int ownerId)
    {
        Name = name;
        PortfolioType = portfolioType;
        OwnerId = ownerId;
    }

    [SetsRequiredMembers]
    public Portfolio(int id, string name, PortfolioType portfolioType, int ownerId) : base(id)
    {
        Name = name;
        PortfolioType = portfolioType;
        OwnerId = ownerId;
    }

    public string Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public PortfolioType PortfolioType { get; set; }
    public int? ParentPortfolioId { get; set; }
    public virtual Portfolio? ParentPortfolio { get; set; }
    public virtual ICollection<Portfolio> ChildrenPortfolios { get; set; } = [];
    public required int OwnerId { get; set; }
    public virtual User? Owner { get; set; }
}