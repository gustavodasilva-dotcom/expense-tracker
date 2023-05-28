using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Services.Account.Interfaces;

public interface IAccountService
{
    Task<UserModel> GetAsync(string id);
    Task<UserModel> SignUpAsync(UserModel user);
    Task<UserModel> UpdateAsync(UserModel user);
}