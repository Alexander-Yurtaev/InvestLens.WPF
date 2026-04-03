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
        ContentHeaderVm = new ContentHeaderViewModel(welcomeTitle, welcomeDescription, OnCreatePortfolio, "+ Создать портфель");

        _portfoliosManager = portfoliosManager;
        PortfolioInfos = _portfoliosManager.PortfolioInfos.Select(p => new PortfolioInfoWrapper(p)).ToList();
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public List<PortfolioInfoWrapper> PortfolioInfos { get; }

    private void OnCreatePortfolio()
    {

    }
}