using AutoMapper;
using InvestLens.Shared.DataAccess.Services;
using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Services;
using InvestLens.ViewModel.Pages;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Factories
{
    public interface ICreatePortfolioViewModelFactory
    {
        IPortfolioDetailViewModel CreatePortfolioDetailViewModel(
            IMapper mapper, 
            PortfolioDetails model, 
            IWindowManager windowManager, 
            IAuthManager authManager, 
            IMetricsManager metricsService, 
            IPortfoliosManager portfoliosManager, 
            ISecurityService securityService,
            IMoexService moexService);
    }
}