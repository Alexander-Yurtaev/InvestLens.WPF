using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesViewModel : ViewModelBaseWithContentHeader, IDictionariesViewModel
{
    public DictionariesViewModel(IDictionariesManager dictionariesManager) : base("Справочники",
        "Источники рыночных данных и справочной информации")
    {
        Cards.AddRange(dictionariesManager.Cards.Select(c => new CardWrapper(c)));
    }

    public List<CardWrapper> Cards { get; } = [];
}