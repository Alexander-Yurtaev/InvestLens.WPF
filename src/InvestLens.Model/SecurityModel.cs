using InvestLens.Model.Enums;

namespace InvestLens.Model;

public class SecurityModel
{
    public SecurityModel(string secId, string name, SecurityType secType)
    {
        SecId = secId;
        Name = name;
        SecType = secType;
    }

    public string SecId { get; }
    public string Name { get; }
    public SecurityType SecType { get; }
}