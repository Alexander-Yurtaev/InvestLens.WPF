using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesViewModel : BaseViewModel, IDictionariesViewModel
{
    private readonly IDictionariesManager _dictionariesManager;

    public DictionariesViewModel(IDictionariesManager dictionariesManager) : base("Справочники",
        "Источники рыночных данных и справочной информации")
    {
        _dictionariesManager = dictionariesManager;
        Cards.AddRange(_dictionariesManager.Cards.Select(c => new CardWrapper(c)));
    }

    public List<CardWrapper> Cards { get; } = [];
}