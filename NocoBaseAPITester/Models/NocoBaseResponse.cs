using System.Text.Json.Serialization;

namespace NocoBaseAPITester.Models
{
    public class NocoBaseResponse
    {
        [JsonPropertyName("data")]
        public NocoBaseData Data { get; set; }
        [JsonPropertyName("meta")]
        public MetaData Meta {  get; set; }
    }

    public class MetaData
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPage")]
        public int TotalPage { get; set; }
    }
}
