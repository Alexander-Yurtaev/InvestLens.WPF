using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfoliosViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
}