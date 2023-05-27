using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Services.Account.Interfaces;

public interface IAccountService
{
    Task<UserModel> SignUpAsync(UserModel user);
}