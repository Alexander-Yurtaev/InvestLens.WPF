using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesMoexManager
{
    List<string> GetSecurityTypes();
    List<Security> GetSecurities(string type);
    List<PeriodType> GetBondPeriodTypes();
    List<Bond> GetBonds(PeriodType periodType);
}