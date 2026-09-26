using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheBabyCowboy.HomeApi.Models;

public class Thought
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("text")]
    public string Text { get; set; } = string.Empty;
}