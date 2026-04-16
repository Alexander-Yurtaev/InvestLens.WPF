using InvestLens.DataAccess.Repositories;
using InvestLens.Model;
using InvestLens.Model.Crud.Portfolio;
using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : ViewModelBaseWithContentHeader, IPortfolioDetailViewModel
{
    private readonly PortfolioDetail _model;
    private readonly IWindowManager _windowManager;
    private readonly IAuthManager _authManager;
    private readonly IPortfoliosManager _portfoliosManager;

    public PortfolioDetailViewModel(
        PortfolioDetail model, 
        IWindowManager windowManager, 
        IAuthManager authManager,
        IPortfoliosManager portfoliosManager) : base(model.Title)
    {
        _model = model;
        _windowManager = windowManager;
        _authManager = authManager;
        _portfoliosManager = portfoliosManager;
        
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
        var editModel = new UpdateModel(_model.Id, _model.Title, _model.PortfolioType);

        var viewModel = new UpdatePortfolioWindowViewModel(editModel, _windowManager, _authManager, _portfoliosManager);
        _windowManager.ShowWindow(viewModel, asDialog: true);
    }

    private void OnImportPortfolio()
    {
        
    }
}