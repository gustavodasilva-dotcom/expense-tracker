using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MVCExpenseTracker.Database.Models;

public class ExpenseModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string id { get; set; }
    public DateTime date { get; set; }
    public string comments { get; set; }
    public ExpenseTypeModel expenseType { get; set; }
    public UserModel user { get; set; }
}