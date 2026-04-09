using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexSecuritiesViewModel : IViewModelBaseWithContentHeader
{
    ObservableCollection<TabItemViewModel> Tabs { get; }
    TabItemViewModel? SelectedTab { get; set; }
    void Load();
}