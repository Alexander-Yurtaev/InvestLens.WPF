using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : INotifyPropertyChanged
{
    string Title { get; }
}