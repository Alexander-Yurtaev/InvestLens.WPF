using InvestLens.Model;
using InvestLens.Model.Enums;

namespace InvestLens.ViewModel.Services;

public class DictionariesMoexManager : IDictionariesMoexManager
{
    private readonly List<Security> _securities =
    [
        new Security("SBER", "Сбербанк", "Акции"),
        new Security("GAZP", "Газпром", "Акции"),
        new Security("LKOH", "Лукойл", "Акции"),
        new Security("FXRL", "FXRL (Россия)", "ETF"),
        new Security("YNDX", "Яндекс", "Акции")
    ];

    private readonly List<Bond> _bonds =
    [
        new Bond("ОФЗ 26233", "ОФЗ 26233", "Минфин РФ", PeriodType.Long),
        new Bond("ОФЗ 26243", "ОФЗ 26243", "Минфин РФ", PeriodType.Long),
        new Bond("СберБ 001P", "СберБ 001P", "Сбербанк", PeriodType.Middle),
        new Bond("Газпром БО-07", "СберБ 001P", "Газпром", PeriodType.Short)
    ];

    public List<string> GetSecurityTypes()
    {
        return _securities.Select(s => s.SecType).Distinct().ToList();
    }

    public List<Security> GetSecurities(string type)
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