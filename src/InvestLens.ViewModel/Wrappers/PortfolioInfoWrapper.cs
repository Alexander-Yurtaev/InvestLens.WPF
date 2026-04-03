using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Wrappers;

public class PortfolioInfoWrapper : BindableBase
{
    private readonly PortfolioInfo _model;

    public PortfolioInfoWrapper(PortfolioInfo model)
    {
        _model = model;
        PortfolioStats = model.PortfolioStats.Select(p => new PortfolioStatsWrapper(p)).ToList();
    }

    public string Title => _model.Title;
    public PortfolioType PortfolioType => _model.PortfolioType;
    public List<PortfolioStatsWrapper> PortfolioStats { get; }
    public string RefreshDate => _model.RefreshDate;
}