using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesMoexViewModel : BaseViewModel, IDictionariesMoexViewModel
{
    public DictionariesMoexViewModel(IMoexService moexService) : base("Московская Биржа (MOEX)",
        "Основные рыночные инструменты и индексы")
    {
        Cards.AddRange(moexService.Cards.Select(c => new CardWrapper(c)));
    }

    public List<CardWrapper> Cards { get; } = [];
}