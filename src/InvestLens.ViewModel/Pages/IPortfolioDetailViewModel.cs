using InvestLens.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : IViewModelBaseWithContentHeader
{
    string Title { get; }
    ICollectionView SecuritiesView { get; }
    ObservableCollection<SecurityOperation> Operations { get; }
}