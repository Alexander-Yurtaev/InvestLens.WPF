using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public interface IDictionariesDohodBondsViewModel : IViewModelBaseWithContentHeader
{
    List<BondWrapper> Bonds { get; }
}