namespace InvestLens.Model;

public class Stat
{
    public Stat(string title, double value, string unit="", bool unitIsSuffix=true)
    {
        Title = title;
        Value = value;
        Unit = unit;
        UnitIsSuffix = unitIsSuffix;
    }

    public string Title { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
    public bool UnitIsSuffix { get; set; }
}