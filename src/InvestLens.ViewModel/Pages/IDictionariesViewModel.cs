using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesViewModel : IViewModelBaseWithContentHeader
{
    List<CardWrapper> Cards { get; }
}