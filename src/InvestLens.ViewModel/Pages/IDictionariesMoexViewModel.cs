using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesMoexViewModel : IViewModelBaseWithContentHeader
{
    List<CardWrapper> Cards { get; }
}