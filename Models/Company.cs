
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DotnetMongo.Models;

public class Company 
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string CompanyName { get; set; } = "";

    public int founded_year { get; set; }
}