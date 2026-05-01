using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.NavigationTree;
using InvestLens.Model.Services;
using InvestLens.ViewModel.Factories;
using InvestLens.ViewModel.Pages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;

namespace InvestLens.ViewModel.Services;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPortfoliosManager _portfoliosManager;
    private readonly IDohodService _dohodService;
    private readonly ICreatePortfolioViewModelFactory _createPortfolioViewModelFactory;
    private readonly IMapper _mapper;
    private readonly IWindowManager _windowManager;
    private readonly IAuthManager _authManager;
    private readonly IMetricsService _metricsService;
    private readonly ISecurityService _securityService;

    public ViewModelFactory(IServiceProvider serviceProvider, 
        IPortfoliosManager portfoliosManager,
        IDohodService dohodService,
        ICreatePortfolioViewModelFactory createPortfolioViewModelFactory,
        IMapper mapper,
        IWindowManager windowManager,
        IAuthManager authManager,
        IMetricsService metricsService,
        ISecurityService securityService)
    {
        _serviceProvider = serviceProvider;
        _portfoliosManager = portfoliosManager;
        _dohodService = dohodService;
        _createPortfolioViewModelFactory = createPortfolioViewModelFactory;
        _mapper = mapper;
        _windowManager = windowManager;
        _authManager = authManager;
        _metricsService = metricsService;
        _securityService = securityService;
    }

    public async Task<INotifyPropertyChanged> CreateViewModel(BaseNavigationTreeModel model)
    {
        return model switch
        {
            DashboardNavigationTreeModel => _serviceProvider.GetRequiredService<IDashboardViewModel>(),
            PortfoliosNavigationTreeModel => _serviceProvider.GetRequiredService<IPortfoliosViewModel>(),

            PortfolioNavigationTreeModel portfolioModel => await CreatePortfolioViewModel(portfolioModel),

            DictionariesNavigationTreeModel => _serviceProvider.GetRequiredService<IDictionariesViewModel>(),
            DictionariesMoexNavigationTreeModel => _serviceProvider.GetRequiredService<IDictionariesMoexViewModel>(),
            DictionariesMoexSecuritiesNavigationTreeModel => _serviceProvider.GetRequiredService<IDictionariesMoexSecuritiesViewModel>(),
            DictionariesMoexBondsNavigationTreeModel => _serviceProvider.GetRequiredService<IDictionariesMoexBondsViewModel>(),
            
            DictionariesDohodNavigationTreeModel => _serviceProvider.GetRequiredService<IDictionariesDohodViewModel>(),
            DictionariesDohodBondNavigationTreeModel dohodModel => CreateDictionariesDohodBondsViewModel(dohodModel),

            DownloaderNavigationTreeModel => _serviceProvider.GetRequiredService<IDownloaderViewModel>(),
            SchedulerNavigationTreeModel => _serviceProvider.GetRequiredService<ISchedulerViewModel>(),
            SettingsNavigationTreeModel => _serviceProvider.GetRequiredService<ISettingsViewModel>(),
            SettingsCommonNavigationTreeModel => _serviceProvider.GetRequiredService<ISettingsCommonViewModel>(),
            SettingsPluginsNavigationTreeModel => _serviceProvider.GetRequiredService<ISettingsPluginsViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(model), model, null)
        };
    }

    private async Task<INotifyPropertyChanged> CreatePortfolioViewModel(PortfolioNavigationTreeModel model)
    {
        var portfolioModel = await _portfoliosManager.GetPortfolioDetiails(model.Id) 
            ?? 
            throw new ArgumentException(nameof(model));

        var viewModel = _createPortfolioViewModelFactory.CreatePortfolioDetailViewModel(
            _mapper, portfolioModel, _windowManager, _authManager, 
            _metricsService, _portfoliosManager, _securityService);
        return viewModel;
    }

    private INotifyPropertyChanged CreateDictionariesDohodBondsViewModel(DictionariesDohodBondNavigationTreeModel model)
    {
        var bonds = _dohodService.GetBonds(model.PeriodType) 
            ?? 
            throw new ArgumentException(nameof(model));
        
        var viewModel = ActivatorUtilities.CreateInstance<IDictionariesDohodBondsViewModel>(
            _serviceProvider, bonds);
        return viewModel;
    }
}