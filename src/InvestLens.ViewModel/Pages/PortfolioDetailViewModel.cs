using InvestLens.Model;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : BindableBase, IPortfolioDetailViewModel
{
    private readonly PortfolioDetail _model;

    public PortfolioDetailViewModel(PortfolioDetail model)
    {
        _model = model;
        var buttonModels = new List<ButtonModel>
        {
            new ButtonModel("Редактировать", OnEditPortfolio),
            new ButtonModel("Импортировать", OnImportPortfolio),
        };
        ContentHeaderVm = new ContentHeaderViewModel(_model.Title, string.Empty, buttonModels);
    }

    public IContentHeaderViewModel ContentHeaderVm { get; }
    public string Title => _model.Title + "-title";

    private void OnEditPortfolio()
    {
        
    }

    private void OnImportPortfolio()
    {
        
    }
}