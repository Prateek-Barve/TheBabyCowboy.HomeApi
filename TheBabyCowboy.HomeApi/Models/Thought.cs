//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

//namespace TheBabyCowboy.HomeApi.Models;

//public class Thought
//{
//    [BsonId]
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string Id { get; set; } = string.Empty;

//    [BsonElement("text")]
//    public string Text { get; set; } = string.Empty;
//}
using Newtonsoft.Json;

namespace TheBabyCowboy.HomeApi.Models;

public class Thought
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("sequence")]
    public int Sequence { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;

    [JsonProperty("saidBy")]
    public string? SaidBy { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
}