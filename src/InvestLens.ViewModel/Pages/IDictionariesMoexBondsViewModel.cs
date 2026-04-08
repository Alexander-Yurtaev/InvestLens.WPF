using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexBondsViewModel : IViewModelBaseWithContentHeader
{
    ObservableCollection<TabItemViewModel> Tabs { get; }
}