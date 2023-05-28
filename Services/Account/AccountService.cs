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

    public async Task<UserModel> GetAsync(string id)
    {
        var userFound = await _mongoDbConnection.GetAsync<UserModel>(c => c.id == id);

        if (userFound == null)
            throw new Exception("User not found");

        return userFound;
    }

    public async Task<UserModel> SignUpAsync(UserModel user)
    {
        var userFound = await _mongoDbConnection.GetAsync<UserModel>(c => c.email == user.email);

        if (userFound == null)
            throw new Exception("User not found");

        if (!user.password.Trim().Equals(userFound.password.DecryptString()))
            throw new Exception("Invalid password");

        return userFound;
    }

    public async Task<UserModel> UpdateAsync(UserModel user)
    {
        var userFound = await _mongoDbConnection.GetAsync<UserModel>(c => c.id == user.id);

        if (userFound == null)
            throw new Exception("User not found");

        await _mongoDbConnection.UpdateAsync<UserModel>(user);

        return await GetAsync(user.id);
    }
}