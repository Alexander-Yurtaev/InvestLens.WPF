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
        switch (nodeType)
        {
            case NodeTypes.Dashboard:
                return _componentContext.Resolve<IDashboardViewModel>();

            case NodeTypes.Portfolios:
                return _componentContext.Resolve<IPortfoliosViewModel>();
            case NodeTypes.PortfoliosComplex:
                return _componentContext.Resolve<IPortfoliosComplexViewModel>();
            case NodeTypes.PortfoliosFirst:
                return _componentContext.Resolve<IPortfoliosFirstViewModel>();
            case NodeTypes.PortfoliosSecond:
                return _componentContext.Resolve<IPortfoliosSecondViewModel>();

            case NodeTypes.Dictionaries:
                return _componentContext.Resolve<IDictionariesViewModel>();
            case NodeTypes.DictionariesMoex:
                return _componentContext.Resolve<IDictionariesMoexViewModel>();
            case NodeTypes.DictionariesMoexSecurities:
                return _componentContext.Resolve<IDictionariesMoexSecuritiesViewModel>();
            case NodeTypes.DictionariesMoexBonds:
                return _componentContext.Resolve<IDictionariesMoexBondsViewModel>();

            case NodeTypes.DictionariesDohod:
                return _componentContext.Resolve<IDictionariesDohodViewModel>();
            case NodeTypes.DictionariesDohodBonds:
                return _componentContext.Resolve<IDictionariesDohodBondsViewModel>();

            case NodeTypes.Downloader:
                return _componentContext.Resolve<IDownloaderViewModel>();
            case NodeTypes.Scheduler:
                return _componentContext.Resolve<ISchedulerViewModel>();

            case NodeTypes.Settings:
                return _componentContext.Resolve<ISettingsViewModel>();
            case NodeTypes.SettingsCommon:
                return _componentContext.Resolve<ISettingsCommonViewModel>();
            case NodeTypes.SettingsPlugins:
                return _componentContext.Resolve<ISettingsPluginsViewModel>();
            default:
                throw new ArgumentOutOfRangeException(nameof(nodeType), nodeType, null);
        }
    }
}