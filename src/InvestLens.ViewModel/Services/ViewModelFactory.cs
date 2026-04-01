using InvestLens.Model.Enums;
using InvestLens.ViewModel.Pages;
using System.ComponentModel;
using Autofac;

namespace InvestLens.ViewModel.Services;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IComponentContext _componentContext;

    public ViewModelFactory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public INotifyPropertyChanged CreateViewModel(NodeTypes nodeType)
    {
        return nodeType switch
        {
            NodeTypes.Dashboard => _componentContext.Resolve<IDashboardViewModel>(),
            NodeTypes.Portfolios => _componentContext.Resolve<IPortfoliosViewModel>(),
            NodeTypes.PortfoliosComplex => _componentContext.Resolve<IPortfoliosComplexViewModel>(),
            NodeTypes.PortfoliosFirst => _componentContext.Resolve<IPortfoliosFirstViewModel>(),
            NodeTypes.PortfoliosSecond => _componentContext.Resolve<IPortfoliosSecondViewModel>(),
            NodeTypes.Dictionaries => _componentContext.Resolve<IDictionariesViewModel>(),
            NodeTypes.DictionariesMoex => _componentContext.Resolve<IDictionariesMoexViewModel>(),
            NodeTypes.DictionariesMoexSecurities => _componentContext.Resolve<IDictionariesMoexSecuritiesViewModel>(),
            NodeTypes.DictionariesMoexBonds => _componentContext.Resolve<IDictionariesMoexBondsViewModel>(),
            NodeTypes.DictionariesDohod => _componentContext.Resolve<IDictionariesDohodViewModel>(),
            NodeTypes.DictionariesDohodBonds => _componentContext.Resolve<IDictionariesDohodBondsViewModel>(),
            NodeTypes.Downloader => _componentContext.Resolve<IDownloaderViewModel>(),
            NodeTypes.Scheduler => _componentContext.Resolve<ISchedulerViewModel>(),
            NodeTypes.Settings => _componentContext.Resolve<ISettingsViewModel>(),
            NodeTypes.SettingsCommon => _componentContext.Resolve<ISettingsCommonViewModel>(),
            NodeTypes.SettingsPlugins => _componentContext.Resolve<ISettingsPluginsViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, null)
        };
    }
}