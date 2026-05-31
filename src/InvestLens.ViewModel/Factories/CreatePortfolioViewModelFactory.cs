using AutoMapper;
using InvestLens.Shared.DataAccess.Services;
using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Services;
using InvestLens.ViewModel.Pages;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Factories;

public class CreatePortfolioViewModelFactory : ICreatePortfolioViewModelFactory
{
    public IPortfolioDetailViewModel CreatePortfolioDetailViewModel(
        IMapper mapper,
        PortfolioDetails model,
        IWindowManager windowManager,
        IAuthManager authManager,
        IMetricsManager metricsService,
        IPortfoliosManager portfoliosManager,
        ISecurityService securityService,
        IMoexService moexService
        )
    {
        return new PortfolioDetailViewModel(mapper,
                                            model,
                                            windowManager,
                                            authManager,
                                            metricsService,
                                            portfoliosManager,
                                            securityService,
                                            moexService);
    }
}