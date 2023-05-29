using MongoDB.Driver;
using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Database.Seeders;

public static class ExpenseTypesSeeder
{
    private static readonly List<String> _expenseTypes = new List<string>()
    {
        "Housing expense",
        "Food expense",
        "Transportation expense",
        "Clothing expense",
        "Health care expense",
        "Child care expense",
        "Education expense",
        "Miscellaneous expense"
    };

    public static void Run(IMongoDatabase database)
    {
        try
        {
            var collection = database.GetCollection<ExpenseTypeModel>("expensetypes");
            var expenseTypes = collection.Find<ExpenseTypeModel>(_ => true);

            if (!expenseTypes.Any())
            {
                foreach (var type in _expenseTypes)
                {
                    var expense = new ExpenseTypeModel { description = type };
                    collection.InsertOne(expense);
                }
            }
        }
        catch (Exception e)
        {

        }
    }
}