using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IPortfoliosViewModel : IBaseViewModel
{
    List<CardWrapper> Cards { get; }
}