using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IViewModelBaseWithContentHeader : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
}