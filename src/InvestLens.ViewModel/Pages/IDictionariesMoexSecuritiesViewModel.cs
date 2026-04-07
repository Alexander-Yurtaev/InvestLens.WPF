using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexSecuritiesViewModel : IBaseViewModel
{
    ObservableCollection<TabItemViewModel> Tabs { get; }
}