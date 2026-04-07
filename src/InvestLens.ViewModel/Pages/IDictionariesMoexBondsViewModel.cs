using System.Collections.ObjectModel;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexBondsViewModel : INotifyPropertyChanged
{
    IContentHeaderViewModel ContentHeaderVm { get; }
    ObservableCollection<TabItemViewModel> Tabs { get; }
}