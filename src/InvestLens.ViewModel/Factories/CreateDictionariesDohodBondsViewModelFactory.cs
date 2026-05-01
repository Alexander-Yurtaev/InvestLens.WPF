using InvestLens.Model;
using InvestLens.ViewModel.Pages;

namespace InvestLens.ViewModel.Factories;

public class CreateDictionariesDohodBondsViewModelFactory : ICreateDictionariesDohodBondsViewModelFactory
{
    public IDictionariesDohodBondsViewModel CreateViewModel(DohodBonds bonds)
    {
        return new DictionariesDohodBondsViewModel(bonds);
    }
}