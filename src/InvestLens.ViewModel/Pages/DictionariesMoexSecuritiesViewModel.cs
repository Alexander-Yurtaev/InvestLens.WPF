using System.Collections.ObjectModel;
using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class DictionariesMoexSecuritiesViewModel : BindableBase, IDictionariesMoexSecuritiesViewModel
{
    private readonly IDictionariesManager _dictionariesManager;
    private TabItemViewModel? _selectedTab;

    public DictionariesMoexSecuritiesViewModel(IDictionariesManager dictionariesManager)
    {
        var welcomeTitle = "Ценные бумаги (MOEX)";
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