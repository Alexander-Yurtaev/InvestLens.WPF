using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : IViewModelBaseWithContentHeader
{
    string Title { get; }
    List<StatWrapper> PortfolioStats { get; }
    List<SecurityInfoWrapper> Securities { get; }
    List<SecurityOperation> Operations { get; }
}