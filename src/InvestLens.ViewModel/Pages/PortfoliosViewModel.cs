using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : BaseViewModel, IPortfoliosViewModel
{
    public PortfoliosViewModel(IPortfoliosManager portfoliosManager) : base("Мои портфели",
        "Управляйте своими инвестиционными портфелями")
    {
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

    }
}