using AutoMapper;
using InvestLens.Model;
using InvestLens.Model.Services;
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
        IMetricsService metricsService,
        IPortfoliosManager portfoliosManager,
        ISecurityService securityService
        )
    {
        return new PortfolioDetailViewModel(mapper,
                                            model,
                                            windowManager,
                                            authManager,
                                            metricsService,
                                            portfoliosManager,
                                            securityService);
    }
}