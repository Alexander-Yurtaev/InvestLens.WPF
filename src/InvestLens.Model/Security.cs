namespace InvestLens.Model;

public class Security
{
    public Security(string secId, string name, string secType)
    {
        SecId = secId;
        Name = name;
        SecType = secType;
    }

    public string SecId { get; }
    public string Name { get; }
    public string SecType { get; }
}