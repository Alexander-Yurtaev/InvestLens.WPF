using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class DictionariesMoexManager : IDictionariesMoexManager
{
    private readonly List<SecurityModel> _securities =
    [
        new SecurityModel("SBER", SecurityType.common_share){ Name = "Сбербанк" },
        new SecurityModel("GAZP", SecurityType.common_share) { Name = "Газпром" },
        new SecurityModel("LKOH", SecurityType.common_share) { Name = "Лукойл" },
        new SecurityModel("FXRL", SecurityType.etf_ppif) { Name = "FXRL (Россия)" },
        new SecurityModel("YNDX", SecurityType.common_share) { Name = "Яндекс" }
    ];

    private readonly List<Bond> _bonds =
    [
        new Bond("ОФЗ 26233", "ОФЗ 26233", "Минфин РФ", PeriodType.Long),
        new Bond("ОФЗ 26243", "ОФЗ 26243", "Минфин РФ", PeriodType.Long),
        new Bond("СберБ 001P", "СберБ 001P", "Сбербанк", PeriodType.Middle),
        new Bond("Газпром БО-07", "СберБ 001P", "Газпром", PeriodType.Short)
    ];

    public List<SecurityType> GetSecurityTypes()
    {
        return _securities
            .Select(s => s.SecType)
            .Distinct()
            .ToList();
    }

    public List<SecurityModel> GetSecurities(SecurityType type)
    {
        return _securities.Where(r => r.SecType == type).ToList();
    }

    public List<PeriodType> GetBondPeriodTypes()
    {
        return _bonds.Select(s => s.PeriodType).Distinct().ToList();
    }

    public List<Bond> GetBonds(PeriodType periodType)
    {
        return _bonds.Where(r => r.PeriodType == periodType).ToList();
    }
}