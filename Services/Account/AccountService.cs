using MVCExpenseTracker.Database.Interfaces;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Services.Account.Interfaces;
using MVCExpenseTracker.Utils;

namespace MVCExpenseTracker.Services.Account;

public class AccountService : IAccountService
{
    private readonly IMongoDbConnection _mongoDbConnection;

    public AccountService(IMongoDbConnection mongoDbConnection)
    {
        _mongoDbConnection = mongoDbConnection;
    }

    public async Task SignUpAsync(UserModel user)
    {
        var userFound = await _mongoDbConnection.GetAsync<UserModel>(c => c.Email == user.Email);

        if (userFound == null)
            throw new Exception("User not found");

        if (!user.Password.Trim().Equals(userFound.Password.DecryptString()))
            throw new Exception("Invalid password");
    }
}