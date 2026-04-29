using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesMoexManager
{
    List<SecurityType> GetSecurityTypes();
    List<SecurityModel> GetSecurities(SecurityType type);
    List<PeriodType> GetBondPeriodTypes();
    List<Bond> GetBonds(PeriodType periodType);
}