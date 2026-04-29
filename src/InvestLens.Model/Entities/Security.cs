using InvestLens.Model.Enums;
using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model.Entities;

public class Security : BaseEntity
{
    [SetsRequiredMembers]
    public Security(string secId)
    {
        SecId = secId;
    }

    public required string SecId { get; set; }
    public string Name { get; } = string.Empty;
    public SecurityType SecType { get; } = SecurityType.None;
    public bool IsLoaded { get; set; }
}
