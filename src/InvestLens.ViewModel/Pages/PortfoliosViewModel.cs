using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : ViewModelBaseWithContentHeader, IPortfoliosViewModel
{
    private readonly IWindowManager _windowManager;

    public PortfoliosViewModel(IPortfoliosManager portfoliosManager, IWindowManager windowManager) : base("Мои портфели",
        "Управляйте своими инвестиционными портфелями")
    {
        _windowManager = windowManager;
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("+ Создать портфель", OnCreatePortfolio)
        };
        ContentHeaderVm.AddButtons(buttonModels);

        Cards.AddRange(portfoliosManager.Cards.Select(c => new CardWrapper(c)));
    }

    public List<CardWrapper> Cards { get; } = [];

    private void OnCreatePortfolio()
    {
        _windowManager.ShowWindow<CreateEditPortfolioWindowViewModel>(true);
    }
}