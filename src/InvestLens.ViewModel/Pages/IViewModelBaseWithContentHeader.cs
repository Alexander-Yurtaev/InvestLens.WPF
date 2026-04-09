using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IViewModelBaseWithContentHeader : INotifyPropertyChanged
{
    public IContentHeaderViewModel ContentHeaderVm { get; }
}