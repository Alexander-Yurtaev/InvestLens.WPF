using InvestLens.Model.Entities.Settings;
using InvestLens.Model.Enums;
using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model.Entities;

public class Security : BaseEntity
{
    public required string SecId { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string? RegNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Isin { get; set; } = string.Empty;
    public bool IsTraded { get; set; }
    public string? EmitentTitle { get; set; } = string.Empty;
    public int? SecTypeId { get; set; }
    public virtual SecurityType? SecType { get; set; }
    public int? SecGroupId { get; set; }
    public virtual SecurityGroup? SecGroup { get; set; }
    public string PrimaryBoardid { get; set; } = string.Empty;
    public string? MarketpriceBoardid { get; set; }
    public bool IsLoaded { get; set; }
}
