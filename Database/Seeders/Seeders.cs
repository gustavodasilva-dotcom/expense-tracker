using MongoDB.Driver;

namespace MVCExpenseTracker.Database.Seeders;

public static class Seeders
{
    public static void Run(string connectionString, string database)
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger(string.Empty);

        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(database);

        UserSeeder.Run(db);
        ExpenseTypesSeeder.Run(db);
    }
}