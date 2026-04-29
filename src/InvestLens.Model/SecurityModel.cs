using InvestLens.Model.Enums;
using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model;

public class SecurityModel
{
    [SetsRequiredMembers]
    public SecurityModel(string secId, SecurityType secType)
    {
        SecId = secId;
        SecType = secType;
    }

    public required string SecId { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string RegNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Isin { get; set; } = string.Empty;
    public string IsTraded { get; set; } = string.Empty;
    public string EmitentTitle { get; set; } = string.Empty;
    public SecurityType SecType { get; } = SecurityType.None;
    public SecurityGroup SecGroup { get; } = SecurityGroup.None;
    public string PrimaryBoardid { get; set; } = string.Empty;
    public string MarketpriceBoardid { get; set; } = string.Empty;
}