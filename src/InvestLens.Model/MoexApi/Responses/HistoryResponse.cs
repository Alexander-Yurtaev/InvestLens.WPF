using System.Text.Json.Serialization;

namespace InvestLens.Model.MoexApi.Responses
{
    public class HistoryResponse
    {
        [JsonPropertyName("history")]
        public ResponseItems.History? History { get; set; }

        [JsonPropertyName("history.cursor")]
        public ResponseItems.HistoryCursor? HistoryCursor { get; set; }
    }
}
