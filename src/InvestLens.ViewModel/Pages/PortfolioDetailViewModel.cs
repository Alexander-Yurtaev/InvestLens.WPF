using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : BaseViewModel, IPortfolioDetailViewModel
{
    private readonly PortfolioDetail _model;

    public PortfolioDetailViewModel(PortfolioDetail model) : base(model.Title)
    {
        _model = model;

        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("Редактировать", OnEditPortfolio),
            new ButtonModel("Импортировать", OnImportPortfolio),
        };
        ContentHeaderVm.Buttons.Clear();
        ContentHeaderVm.AddButtons(buttonModels);

        PortfolioStats = model.PortfolioStats.Select(p => new StatWrapper(p)).ToList();
        Securities = _model.Securities.Select(s => new SecurityInfoWrapper(s)).ToList();
    }

    public string Title => _model.Title;
    public List<StatWrapper> PortfolioStats { get; }
    public List<SecurityInfoWrapper> Securities { get; }
    public List<SecurityOperation> Operations => _model.Operations;

    private void OnEditPortfolio()
    {
        
    }

    private void OnImportPortfolio()
    {
        
    }
}