using Autofac;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.Pages;
using System.ComponentModel;
using InvestLens.Model;

namespace InvestLens.ViewModel.Services;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IComponentContext _componentContext;
    private readonly IPortfoliosManager _portfoliosManager;

    public ViewModelFactory(IComponentContext componentContext, IPortfoliosManager portfoliosManager)
    {
        _componentContext = componentContext;
        _portfoliosManager = portfoliosManager;
    }

    public INotifyPropertyChanged CreateViewModel(NodeType nodeType)
    {
        return nodeType switch
        {
            NodeType.Dashboard => _componentContext.Resolve<IDashboardViewModel>(),
            NodeType.Portfolios => _componentContext.Resolve<IPortfoliosViewModel>(),

            NodeType.PortfoliosComplex => CreatePortfolioViewModel(nodeType),
            NodeType.PortfoliosFirst => CreatePortfolioViewModel(nodeType),
            NodeType.PortfoliosSecond => CreatePortfolioViewModel(nodeType),
            
            NodeType.Dictionaries => _componentContext.Resolve<IDictionariesViewModel>(),
            NodeType.DictionariesMoex => _componentContext.Resolve<IDictionariesMoexViewModel>(),
            NodeType.DictionariesMoexSecurities => _componentContext.Resolve<IDictionariesMoexSecuritiesViewModel>(),
            NodeType.DictionariesMoexBonds => _componentContext.Resolve<IDictionariesMoexBondsViewModel>(),
            NodeType.DictionariesDohod => _componentContext.Resolve<IDictionariesDohodViewModel>(),
            NodeType.DictionariesDohodBonds => _componentContext.Resolve<IDictionariesDohodBondsViewModel>(),
            NodeType.Downloader => _componentContext.Resolve<IDownloaderViewModel>(),
            NodeType.Scheduler => _componentContext.Resolve<ISchedulerViewModel>(),
            NodeType.Settings => _componentContext.Resolve<ISettingsViewModel>(),
            NodeType.SettingsCommon => _componentContext.Resolve<ISettingsCommonViewModel>(),
            NodeType.SettingsPlugins => _componentContext.Resolve<ISettingsPluginsViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, null)
        };
    }

    private INotifyPropertyChanged CreatePortfolioViewModel(NodeType nodeType)
    {
        var model = _portfoliosManager.GetPortfolio(nodeType);
        var parameters = new TypedParameter(typeof(PortfolioDetail), model);
        var viewModel = _componentContext.Resolve<IPortfolioDetailViewModel>(parameters);
        return viewModel;
    }
}