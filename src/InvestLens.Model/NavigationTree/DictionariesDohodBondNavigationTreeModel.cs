using InvestLens.Model.Enums;

namespace InvestLens.Model.NavigationTree;

public class DictionariesDohodBondNavigationTreeModel(PeriodType periodType) : BaseNavigationTreeModel
{
    public PeriodType PeriodType { get; } = periodType;
}