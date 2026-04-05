using InvestLens.Model;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : BindableBase, IPortfolioDetailViewModel
{
    private readonly PortfolioDetail _model;

    public PortfolioDetailViewModel(PortfolioDetail model)
    {
        _model = model;
    }

    public string Title => _model.Title;
}