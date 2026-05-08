using System.Text.Json.Serialization;

namespace InvestLens.Model.MoexApi;

public class HistoryCursor
{
    [JsonPropertyName("INDEX")]
    public long Index { get; set; }

    [JsonPropertyName("TOTAL")]
    public long Total { get; set; }

    [JsonPropertyName("PAGESIZE")]
    public long PageSize { get; set; }
}