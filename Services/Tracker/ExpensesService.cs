using MVCExpenseTracker.Database.Interfaces;
using MVCExpenseTracker.Database.Models;
using MVCExpenseTracker.Services.Tracker.Interfaces;

namespace MVCExpenseTracker.Services.Tracker;

public class ExpensesService : IExpensesService
{
    private readonly IMongoDbConnection _mongoDbConnection;

    public ExpensesService(IMongoDbConnection mongoDbConnection)
    {
        _mongoDbConnection = mongoDbConnection;
    }

    public async Task<List<ExpenseTypeModel>> GetExpenseTypesAsync()
    {
        return await _mongoDbConnection.GetAllAsync<ExpenseTypeModel>();
    }

    public async Task<List<ExpenseModel>> GetExpensesAsync(string userId)
    {
        return await _mongoDbConnection.GetAllAsync<ExpenseModel>(e => e.user.id == userId);
    }

    public async Task<ExpenseModel> AddExpenseAsync(ExpenseModel expense)
    {
        expense.user = await _mongoDbConnection
            .GetAsync<UserModel>(u => u.id == expense.user.id);

        expense.expenseType = await _mongoDbConnection
            .GetAsync<ExpenseTypeModel>(u => u.id == expense.expenseType.id);

        var expenseId = await _mongoDbConnection.InsertAsync<ExpenseModel>(expense);
        return await _mongoDbConnection.GetAsync<ExpenseModel>(e => e.id == expenseId.ToString());
    }

    public async Task DeleteAsync(string expenseId)
    {
        var expense = await _mongoDbConnection.GetAsync<ExpenseModel>(e => e.id == expenseId);

        if (expense == null)
            throw new Exception("Expense not found");

        await _mongoDbConnection.DeleteAsync<ExpenseModel>(e => e.id == expenseId);
    }
}