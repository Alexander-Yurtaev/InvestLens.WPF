using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexSecuritiesViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
    ObservableCollection<TabItemViewModel> Tabs { get; }
}