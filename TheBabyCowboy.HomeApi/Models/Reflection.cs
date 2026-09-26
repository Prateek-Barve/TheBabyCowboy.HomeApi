using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheBabyCowboy.HomeApi.Models;

public class Reflection
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonRepresentation(BsonType.ObjectId)]
    public string ThoughtId { get; set; } = string.Empty;

    public string VisitorResponse { get; set; } = string.Empty;

    public string AIResponse { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}