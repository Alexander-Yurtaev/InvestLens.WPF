using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class DictionariesMoexManager : IDictionariesMoexManager
{
    private readonly List<SecurityModel> _securities =
    [
        new SecurityModel("SBER", "Сбербанк", SecurityType.Stock),
        new SecurityModel("GAZP", "Газпром", SecurityType.Stock),
        new SecurityModel("LKOH", "Лукойл", SecurityType.Stock),
        new SecurityModel("FXRL", "FXRL (Россия)", SecurityType.ETF),
        new SecurityModel("YNDX", "Яндекс", SecurityType.Stock)
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