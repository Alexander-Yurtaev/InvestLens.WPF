using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IBaseViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
}