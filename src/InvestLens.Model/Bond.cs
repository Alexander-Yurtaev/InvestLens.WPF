using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class Bond
{
    public Bond(string isin, string name, string issuer, PeriodType periodType)
    {
        Isin = isin;
        Name = name;
        PeriodType = periodType;
        Issuer = issuer;
    }

    public string Isin { get; }
    public string Name { get; }
    public string Issuer { get; }
    public PeriodType PeriodType { get; }
}