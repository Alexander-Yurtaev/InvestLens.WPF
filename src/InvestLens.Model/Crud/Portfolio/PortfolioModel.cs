using InvestLens.Model.Entities;
using InvestLens.Model.Enums;

namespace InvestLens.Model.Crud.Portfolio;

public class PortfolioModel : BaseModel
{
    public PortfolioModel() : base(0, string.Empty, PortfolioType.Invest)
    {
        
    }

    public PortfolioModel(int id, string title, PortfolioType portfolioType) : base(id, title, portfolioType)
    {
    }

    public int? ParentPortfolioId { get; set; }
    public virtual PortfolioModel? ParentPortfolio { get; set; }
    public virtual ICollection<PortfolioModel> ChildrenPortfolios { get; set; } = [];
    public required int OwnerId { get; set; }
    public virtual User? Owner { get; set; }
}