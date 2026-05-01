using System.Text.Json.Serialization;

namespace InvestLens.Model.MoexApi.Responses.ResponseItems
{
    public abstract class BaseMoexResponseItem
    {
        [JsonPropertyName("columns")]
        public string[] Columns { get; set; } = [];

        [JsonPropertyName("data")]
        public object[][] Data { get; set; } = [];
    }
}