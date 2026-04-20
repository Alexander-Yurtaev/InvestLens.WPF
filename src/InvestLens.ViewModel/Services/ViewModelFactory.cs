using Autofac;
using InvestLens.Model;
using InvestLens.Model.NavigationTree;
using InvestLens.ViewModel.Pages;
using System.ComponentModel;

namespace InvestLens.ViewModel.Services;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IComponentContext _componentContext;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IDohodService _dohodService;

    public ViewModelFactory(IComponentContext componentContext, 
        IPortfoliosManager portfoliosManager,
        IDohodService dohodService)
    {
        _componentContext = componentContext;
        _portfoliosManager = portfoliosManager;
        _dohodService = dohodService;
    }

    public async Task<INotifyPropertyChanged> CreateViewModel(BaseNavigationTreeModel model)
    {
        return model switch
        {
            DashboardNavigationTreeModel => _componentContext.Resolve<IDashboardViewModel>(),
            PortfoliosNavigationTreeModel => _componentContext.Resolve<IPortfoliosViewModel>(),

            PortfolioNavigationTreeModel portfolioModel => await CreatePortfolioViewModel(portfolioModel),

            DictionariesNavigationTreeModel => _componentContext.Resolve<IDictionariesViewModel>(),
            DictionariesMoexNavigationTreeModel => _componentContext.Resolve<IDictionariesMoexViewModel>(),
            DictionariesMoexSecuritiesNavigationTreeModel => _componentContext.Resolve<IDictionariesMoexSecuritiesViewModel>(),
            DictionariesMoexBondsNavigationTreeModel => _componentContext.Resolve<IDictionariesMoexBondsViewModel>(),
            
            DictionariesDohodNavigationTreeModel => _componentContext.Resolve<IDictionariesDohodViewModel>(),
            DictionariesDohodBondNavigationTreeModel dohodModel => CreateDictionariesDohodBondsViewModel(dohodModel),

            DownloaderNavigationTreeModel => _componentContext.Resolve<IDownloaderViewModel>(),
            SchedulerNavigationTreeModel => _componentContext.Resolve<ISchedulerViewModel>(),
            SettingsNavigationTreeModel => _componentContext.Resolve<ISettingsViewModel>(),
            SettingsCommonNavigationTreeModel => _componentContext.Resolve<ISettingsCommonViewModel>(),
            SettingsPluginsNavigationTreeModel => _componentContext.Resolve<ISettingsPluginsViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }

    private async Task<INotifyPropertyChanged> CreatePortfolioViewModel(PortfolioNavigationTreeModel model)
    {
        var portfolioModel = await _portfoliosManager.GetPortfolioDetiails(model.Id);
        var parameters = new TypedParameter(typeof(PortfolioDetails), portfolioModel);
        var viewModel = _componentContext.Resolve<IPortfolioDetailViewModel>(parameters);
        return viewModel;
    }

    private INotifyPropertyChanged CreateDictionariesDohodBondsViewModel(DictionariesDohodBondNavigationTreeModel model)
    {
        var bonds = _dohodService.GetBonds(model.PeriodType);
        var parameters = new TypedParameter(typeof(DohodBonds), bonds);
        var viewModel = _componentContext.Resolve<IDictionariesDohodBondsViewModel>(parameters);
        return viewModel;
    }
}