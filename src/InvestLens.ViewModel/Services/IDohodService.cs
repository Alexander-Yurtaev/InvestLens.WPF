using InvestLens.Model;
using InvestLens.Model.Enums;
using InvestLens.ViewModel.NavigationTree;

namespace InvestLens.ViewModel.Services;

public interface IDohodService
{
    List<Card> Cards { get; }
    List<INavigationTreeItem> GetDohodBondsMenuItems();
    DohodBonds GetBonds(PeriodType periodType);
}