using System.Linq.Expressions;
using MongoDB.Driver;

namespace MVCExpenseTracker.Database.Interfaces;

public interface IMongoDbConnection
{
    IMongoCollection<T> Connect<T>();
    Task<T> GetAsync<T>(Expression<Func<T, bool>> func);
    Task<List<T>> GetAllAsync<T>();
    Task<MongoDB.Bson.ObjectId> InsertAsync<T>(T entity);
    Task UpdateAsync<T>(T entity);
}