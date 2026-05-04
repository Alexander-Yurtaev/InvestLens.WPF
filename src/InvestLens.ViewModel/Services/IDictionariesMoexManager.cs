using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesMoexManager
{
    List<SecurityTypeModel> GetSecurityTypes();
    List<SecurityModel> GetSecurities(SecurityTypeModel type);
    List<PeriodType> GetBondPeriodTypes();
    List<Bond> GetBonds(PeriodType periodType);
}