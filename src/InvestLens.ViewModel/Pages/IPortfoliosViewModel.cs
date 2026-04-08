using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IPortfoliosViewModel : IViewModelBaseWithContentHeader
{
    List<CardWrapper> Cards { get; }
}