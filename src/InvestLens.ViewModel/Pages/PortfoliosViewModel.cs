using InvestLens.ViewModel.Services;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : BindableBase, IPortfoliosViewModel
{
    private IPortfoliosManager _portfoliosManager;

    public PortfoliosViewModel(IPortfoliosManager portfoliosManager)
    {
        var welcomeTitle = "Мои портфели";
        var welcomeDescription = "Управляйте своими инвестиционными портфелями";
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("+ Создать портфель", OnCreatePortfolio)
        };
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription, buttonModels);

        _portfoliosManager = portfoliosManager;
        Cards.AddRange(_portfoliosManager.Cards.Select(p => new CardWrapper(p)));
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public List<CardWrapper> Cards { get; } = [];

    private void OnCreatePortfolio()
    {

    }
}