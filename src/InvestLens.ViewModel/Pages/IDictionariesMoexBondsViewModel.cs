using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexBondsViewModel : IBaseViewModel
{
    ObservableCollection<TabItemViewModel> Tabs { get; }
}