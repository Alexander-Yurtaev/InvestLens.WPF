using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class DictionariesMoexManager : IDictionariesMoexManager
{
    private readonly List<SecurityModel> _securities =
    [
        new SecurityModel{ SecId = "SBER", SecTypeId = 3, Name = "Сбербанк" },
        new SecurityModel{ SecId = "GAZP", SecTypeId = 3, Name = "Газпром" },
        new SecurityModel{ SecId ="LKOH", SecTypeId = 3, Name = "Лукойл" },
        new SecurityModel{ SecId = "FXRL", SecTypeId = 55, Name = "FXRL (Россия)" },
        new SecurityModel{ SecId = "YNDX", SecTypeId = 3, Name = "Яндекс" }
    ];

    private readonly List<Bond> _bonds =
    [
        new Bond("ОФЗ 26233", "ОФЗ 26233", "Минфин РФ", PeriodType.Long),
        new Bond("ОФЗ 26243", "ОФЗ 26243", "Минфин РФ", PeriodType.Long),
        new Bond("СберБ 001P", "СберБ 001P", "Сбербанк", PeriodType.Middle),
        new Bond("Газпром БО-07", "СберБ 001P", "Газпром", PeriodType.Short)
    ];

    public List<SecurityTypeModel> GetSecurityTypes()
    {
        return _securities
            .Select(s => s.SecType)
            .Where(st => st != null)
            .Distinct()
            .Cast<SecurityTypeModel>()
            .ToList();
    }

    public List<SecurityModel> GetSecurities(SecurityTypeModel type)
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