using InvestLens.ViewModel.Wrappers;
using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfoliosViewModel : IViewModelBaseWithContentHeader, ILoadableViewModel
{
    ObservableCollection<CardWrapper> Cards { get; }
}