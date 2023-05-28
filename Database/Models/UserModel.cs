using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCExpenseTracker.Database.Models;

public class UserModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public float? monthlyIncome { get; set; }
}