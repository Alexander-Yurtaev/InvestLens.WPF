using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesDohodViewModel : IViewModelBaseWithContentHeader
{
    List<CardWrapper> Cards { get; }
}