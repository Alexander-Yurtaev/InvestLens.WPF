using InvestLens.Model.Enums;
using System.ComponentModel;

namespace InvestLens.ViewModel.Pages;

public interface IPortfolioDetailViewModel : INotifyPropertyChanged
{
    string Title { get; }
}

public class PortfolioDetailViewModel : BindableBase, IPortfolioDetailViewModel
{
    private readonly NodeTypes _nodeType;

    public PortfolioDetailViewModel(NodeTypes nodeType)
    {
        _nodeType = nodeType;
    }

    public string Title => _nodeType.ToString();
}