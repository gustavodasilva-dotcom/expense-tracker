using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Services.Account.Interfaces;

public interface IAccountService
{
    Task SignUpAsync(UserModel user);
}