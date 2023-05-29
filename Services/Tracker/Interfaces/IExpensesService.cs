using MVCExpenseTracker.Database.Models;

namespace MVCExpenseTracker.Services.Tracker.Interfaces;

public interface IExpensesService
{
    Task<List<ExpenseTypeModel>> GetExpenseTypesAsync();
    Task AddExpenseAsync(ExpenseModel expense);
}