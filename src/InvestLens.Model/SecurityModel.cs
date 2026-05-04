using System.Diagnostics.CodeAnalysis;

namespace InvestLens.Model;

public class SecurityModel : BaseModel
{
    public string SecId { get; init; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string RegNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Isin { get; set; } = string.Empty;
    public bool IsTraded { get; set; }
    public string EmitentTitle { get; set; } = string.Empty;
    public int? SecTypeId { get; set; }
    public SecurityTypeModel? SecType { get; set; }
    public int? SecGroupId { get; set; }
    public SecurityGroupModel? SecGroup { get; set; }
    public string PrimaryBoardid { get; set; } = string.Empty;
    public string MarketpriceBoardid { get; set; } = string.Empty;
}