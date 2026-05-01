using InvestLens.Model;
using InvestLens.ViewModel.Pages;

namespace InvestLens.ViewModel.Factories
{
    public interface ICreateDictionariesDohodBondsViewModelFactory
    {
        IDictionariesDohodBondsViewModel CreateViewModel(DohodBonds bonds);
    }
}