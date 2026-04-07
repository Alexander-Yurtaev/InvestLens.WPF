using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class Bond
{
    public Bond(string secId, string name, string issuer, PeriodType periodType)
    {
        SecId = secId;
        Name = name;
        PeriodType = periodType;
        Issuer = issuer;
    }

    public string SecId { get; }
    public string Name { get; }
    public string Issuer { get; }
    public PeriodType PeriodType { get; }
}