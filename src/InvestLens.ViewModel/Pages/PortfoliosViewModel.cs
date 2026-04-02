using InvestLens.ViewModel.Services;

namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : BindableBase, IPortfoliosViewModel
{
    public PortfoliosViewModel(IPortfoliosManager portfoliosManager)
    {
        PortfoliosManager = portfoliosManager;
        var welcomeTitle = "Мои портфели";
        var welcomeDescription = "Управляйте своими инвестиционными портфелями";
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription, OnCreatePortfolio, "+ Создать портфель");
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public IPortfoliosManager PortfoliosManager { get; }

    private void OnCreatePortfolio()
    {

    }
}