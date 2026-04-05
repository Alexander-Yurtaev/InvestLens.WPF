using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : BindableBase, IPortfolioDetailViewModel
{
    private readonly PortfolioDetail _model;

    public PortfolioDetailViewModel(PortfolioDetail model)
    {
        _model = model;

        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("Редактировать", OnEditPortfolio),
            new ButtonModel("Импортировать", OnImportPortfolio),
        };
        ContentHeaderVm = new ContentHeaderViewModel(_model.Title, string.Empty, buttonModels);

        PortfolioStats = model.PortfolioStats.Select(p => new PortfolioStatsWrapper(p)).ToList();
        Securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public string Title => _model.Title + "-title";
    public List<PortfolioStatsWrapper> PortfolioStats { get; }
    public List<SecurityInfoWrapper> Securities { get; }
    public List<SecurityOperation> Operations => _model.Operations;

    private void OnEditPortfolio()
    {
        
    }

    private void OnImportPortfolio()
    {
        
    }
}