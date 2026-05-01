using InvestLens.Model.Entities;
using InvestLens.Model.Enums;
using InvestLens.Model.MoexApi.Responses.ResponseItems;
using System.Reflection;
using System.Text.Json.Serialization;

namespace InvestLens.Model.MoexApi.Responses;

public class SecuritiesResponse
{
    [JsonPropertyName("securities")]
    public Securities Securities { get; set; }
}

public class Securities : BaseMoexResponseItem
{
    
}


public record Security
{
    [JsonPropertyName("secid")]
    public string SecId { get; set; } = string.Empty;

    [JsonPropertyName("shortname")]
    public string ShortName { get; set; } = string.Empty;

    [JsonPropertyName("regnumber")]
    public string? RegNumber { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("isin")]
    public string Isin { get; set; } = string.Empty;

    [JsonPropertyName("is_traded")]
    public string IsTraded { get; set; } = string.Empty;

    [JsonPropertyName("emitent_title")]
    public string EmitentTitle { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string SecType { get; set; } = string.Empty;

    [JsonPropertyName("group")]
    public string SecGroup { get; set; } = string.Empty;

    [JsonPropertyName("primary_boardid")]
    public string PrimaryBoardid { get; set; } = string.Empty;

    [JsonPropertyName("marketprice_boardid")]
    public string? MarketpriceBoardid { get; set; }
}