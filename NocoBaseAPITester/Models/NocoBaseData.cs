using System.Text.Json;
using System.Text.Json.Serialization;

namespace NocoBaseAPITester.Models
{
    [JsonConverter(typeof(NocoBaseDataJsonConverter))]
    public class NocoBaseData : List<Dictionary<string, JsonElement>>
    {
        public HashSet<string> Keys
        {
            get
            {
                if (Count == 0)
                {
                    return new HashSet<string>();
                }

                var commonKeys = new HashSet<string>(this[0].Keys);
                for (int i = 1; i < Count; i++)
                {
                    commonKeys.IntersectWith(this[i].Keys);
                }

                return commonKeys;
            }
        }
    }

    public class NocoBaseDataJsonConverter : JsonConverter<NocoBaseData>
    {
        public override NocoBaseData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of array.");
            }

            var data = new NocoBaseData();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return data;
                }

                var item = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader, options);
                data.Add(item);
            }

            throw new JsonException("Expected end of array.");
        }

        public override void Write(Utf8JsonWriter writer, NocoBaseData value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (List<Dictionary<string, JsonElement>>)value, options);
        }
    }
}
