using System.Collections.ObjectModel;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DictionariesMoexSecuritiesViewModel : ViewModelBaseWithContentHeader, IDictionariesMoexSecuritiesViewModel
{
    private readonly IDictionariesManager _dictionariesManager;
    private TabItemViewModel? _selectedTab;

    public DictionariesMoexSecuritiesViewModel(IDictionariesManager dictionariesManager) : base("Ценные бумаги (MOEX)")
    {
        _dictionariesManager = dictionariesManager;
        Load();
        SelectedTab = Tabs.FirstOrDefault();
    }

    public ObservableCollection<TabItemViewModel> Tabs { get; } = [];

    public TabItemViewModel? SelectedTab
    {
        get => _selectedTab;
        set => SetProperty(ref _selectedTab, value);
    }

    public void Load()
    {
        var types = _dictionariesManager.DictionariesMoexManager.GetSecurityTypes();
        foreach (var type in types)
        {
            var securities = _dictionariesManager.DictionariesMoexManager.GetSecurities(type);
            var model = new TabItemViewModel(type)
            {
                Content = securities.Cast<object>().ToList()
            };
            Tabs.Add(model);
        }
    }
}