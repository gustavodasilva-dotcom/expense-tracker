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

    public async Task AddExpenseAsync(ExpenseModel expense)
    {
        var expenseId = await _mongoDbConnection.InsertAsync<ExpenseModel>(expense);
        
    }
}