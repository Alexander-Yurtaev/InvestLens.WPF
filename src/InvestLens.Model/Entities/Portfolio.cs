using InvestLens.Model.Enums;
using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model.Entities;

public class Portfolio : BaseEntity
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public PortfolioType PortfolioType { get; init; }
    public int? ParentPortfolioId { get; set; }
    public virtual Portfolio? ParentPortfolio { get; set; }
    public virtual ICollection<Portfolio> ChildrenPortfolios { get; set; } = [];
    public required int OwnerId { get; init; }
    public virtual User? Owner { get; set; }

    public virtual List<Transaction> Transactions { get; set; } = [];
}