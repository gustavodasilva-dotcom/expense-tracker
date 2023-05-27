using MongoDB.Driver;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Utils;

namespace MVCExpenseTracker.Database.Seeders;

public static class Seeders
{
    public static void Run(string connectionString, string database)
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger(string.Empty);

        try
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(database);

            var collection = db.GetCollection<UserModel>("users");
            var users = collection.Find<UserModel>(_ => true);

            if (!users.Any())
            {
                var adminUser = new UserModel
                {
                    Email = "admin@expensetracker.com",
                    Password = "Admin123@".EncryptString()
                };

                collection.InsertOne(adminUser);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message, e);
        }
    }
}