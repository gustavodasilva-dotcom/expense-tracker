using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCExpenseTracker.Database.Models;

public class ExpenseTypeModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    public string description { get; set; }
}