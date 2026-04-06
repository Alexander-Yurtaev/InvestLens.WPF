using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesMoexViewModel : BindableBase, IDictionariesMoexViewModel
{
    private readonly IMoexService _moexService;

    public DictionariesMoexViewModel(IMoexService moexService)
    {
        var welcomeTitle = "Московская Биржа (MOEX)";
        var welcomeDescription = "Основные рыночные инструменты и индексы";
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription);

        _moexService = moexService;
        Cards.AddRange(_moexService.Cards.Select(c => new CardWrapper(c)));
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public List<CardWrapper> Cards { get; } = [];
}