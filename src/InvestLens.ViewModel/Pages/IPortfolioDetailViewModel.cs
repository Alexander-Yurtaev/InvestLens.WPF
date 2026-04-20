using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : IViewModelBaseWithContentHeader
{
    string Title { get; }
    ObservableCollection<StatWrapper> PortfolioStats { get; }
    ICollectionView SecuritiesView { get; }
    ObservableCollection<SecurityOperation> Operations { get; }
}