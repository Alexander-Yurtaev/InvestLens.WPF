using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesDohodViewModel : ViewModelBaseWithContentHeader, IDictionariesDohodViewModel
{
    public DictionariesDohodViewModel(IDohodService dohodService) : base("Dohod.ru",
        "Агрегатор данных по облигациям")
    {
        Cards.AddRange(dohodService.Cards.Select(c => new CardWrapper(c)));
    }

    public List<CardWrapper> Cards { get; } = [];
}