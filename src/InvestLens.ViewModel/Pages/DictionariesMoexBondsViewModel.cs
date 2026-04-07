using InvestLens.ViewModel.Services;
using System.Collections.ObjectModel;

namespace InvestLens.ViewModel.Pages;

public class DictionariesMoexBondsViewModel : BindableBase, IDictionariesMoexBondsViewModel
{
    private readonly IDictionariesManager _dictionariesManager;
    private TabItemViewModel? _selectedTab;

    public DictionariesMoexBondsViewModel(IDictionariesManager dictionariesManager)
    {
        var welcomeTitle = "Облигации (MOEX)";
        var welcomeDescription = "";
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription);

        _dictionariesManager = dictionariesManager;
        Load();
        SelectedTab = Tabs.FirstOrDefault();
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public ObservableCollection<TabItemViewModel> Tabs { get; } = [];

    public TabItemViewModel? SelectedTab
    {
        get => _selectedTab;
        set => SetProperty(ref _selectedTab, value);
    }

    public void Load()
    {
        var periodTypes = _dictionariesManager.DictionariesMoexManager.GetBondPeriodTypes();
        foreach (var periodType in periodTypes)
        {
            var bonds = _dictionariesManager.DictionariesMoexManager.GetBonds(periodType);
            var model = new TabItemViewModel(periodType.ToString())
            {
                Content = bonds.Cast<object>().ToList()
            };
            Tabs.Add(model);
        }
    }
}