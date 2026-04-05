using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesViewModel : BindableBase, IDictionariesViewModel
{
    private readonly IDictionariesManager _dictionariesManager;

    public DictionariesViewModel(IDictionariesManager dictionariesManager)
    {
        var welcomeTitle = "Справочники";
        var welcomeDescription = "Источники рыночных данных и справочной информации";
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription);

        _dictionariesManager = dictionariesManager;
        Cards.AddRange(_dictionariesManager.Cards.Select(c => new CardWrapper(c)));
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public List<CardWrapper> Cards { get; } = [];
}