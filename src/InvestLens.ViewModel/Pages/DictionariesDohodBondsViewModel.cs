using InvestLens.Model;
using InvestLens.ViewModel.Wrappers;

namespace InvestLens.ViewModel.Pages;

public class DictionariesDohodBondsViewModel : ViewModelBaseWithContentHeader, IDictionariesDohodBondsViewModel
{
    private readonly DohodBonds _model;

    public DictionariesDohodBondsViewModel(DohodBonds model) : base(model.Title)
    {
        _model = model;
        Bonds = _model.Bonds.Select(s => new BondWrapper(s)).ToList();
    }

    public List<BondWrapper> Bonds { get; }
}