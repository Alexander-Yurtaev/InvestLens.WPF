using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : IViewModelBaseWithContentHeader
{
    string Title { get; }
    List<StatWrapper> PortfolioStats { get; }
    ICollectionView SecuritiesView { get; }
    List<SecurityOperation> Operations { get; }
}