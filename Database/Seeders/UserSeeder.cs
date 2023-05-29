using MongoDB.Driver;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Utils;

namespace MVCExpenseTracker.Database.Seeders;

public static class UserSeeder
{
    public static void Run(IMongoDatabase database)
    {
        try
        {
            var collection = database.GetCollection<UserModel>("users");
            var users = collection.Find<UserModel>(_ => true);

            if (!users.Any())
            {
                var adminUser = new UserModel
                {
                    email = "admin@expensetracker.com",
                    password = "Admin123@".EncryptString()
                };

                collection.InsertOne(adminUser);
            }
        }
        catch (Exception e)
        {
            
        }
    }
}