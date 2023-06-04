using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Services.Tracker.Interfaces;

public interface IExpensesService
{
    Task<List<ExpenseTypeModel>> GetExpenseTypesAsync();
    Task<List<ExpenseModel>> GetExpensesAsync(string userId);
    Task<ExpenseModel> AddExpenseAsync(ExpenseModel expense);
    Task DeleteAsync(string expenseId);
}