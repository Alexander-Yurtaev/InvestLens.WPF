using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Pages;

public class PortfolioDetailViewModel : BindableBase, IPortfolioDetailViewModel
{
    private readonly NodeTypes _nodeType;

    public PortfolioDetailViewModel(NodeTypes nodeType)
    {
        _nodeType = nodeType;
    }

    public string Title => _nodeType.ToString();
}