namespace InvestLens.ViewModel.Pages;

public class PortfoliosViewModel : BindableBase, IPortfoliosViewModel
{
    public PortfoliosViewModel()
    {
        var welcomeTitle = "Мои портфели";
        var welcomeDescription = "Управляйте своими инвестиционными портфелями";
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription, OnCreatePortfolio, "+ Создать портфель");
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }

    private void OnCreatePortfolio()
    {

    }
}