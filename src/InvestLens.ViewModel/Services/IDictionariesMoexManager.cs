using InvestLens.Shared.Model;
using InvestLens.Shared.Model.Enums;
using InvestLens.Shared.Model.MoexApi.Settings;

namespace InvestLens.ViewModel.Services;

public interface IDictionariesMoexManager
{
    List<SecurityTypeModel> GetSecurityTypes();
    List<SecurityModel> GetSecurities(SecurityTypeModel type);
    List<PeriodType> GetBondPeriodTypes();
    List<Bond> GetBonds(PeriodType periodType);
}