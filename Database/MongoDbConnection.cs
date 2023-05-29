using System.Linq.Expressions;
using System.Reflection;
using Humanizer;
using MongoDB.Driver;
using MVCExpenseTracker.Database.Interfaces;

namespace MVCExpenseTracker.Database;

public class MongoDbConnection : IMongoDbConnection
{
    private readonly string _connectionString;
    private readonly string _databaseName;

    public MongoDbConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MongoDB");
        _databaseName = configuration["MongoDB:Database"];
    }

    public IMongoCollection<T> Connect<T>()
    {
        var client = new MongoClient(_connectionString);
        var db = client.GetDatabase(_databaseName);

        var modelName = typeof(T)?.FullName?.Replace("Model", "").Split('.').Last();
        var collection = modelName.Pluralize().ToLower();

        return db.GetCollection<T>(collection);
    }

    public async Task<T> GetAsync<T>(Expression<Func<T, bool>> func)
    {
        var collection = Connect<T>();
        var result = await collection.FindAsync(func);
        return result.FirstOrDefault();
    }

    public async Task<List<T>> GetAllAsync<T>()
    {
        var collection = Connect<T>();
        var results = await collection.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<MongoDB.Bson.ObjectId> InsertAsync<T>(T entity)
    {
        var collection = Connect<T>();

        var id = MongoDB.Bson.ObjectId.GenerateNewId();

        var type = entity?.GetType();
        PropertyInfo prop = type?.GetProperty("id")!;
        prop.SetValue(entity, id, null);

        await collection.InsertOneAsync(entity);

        return id;
    }

    public Task UpdateAsync<T>(T entity)
    {
        var collection = Connect<T>();

        var type = entity?.GetType();
        PropertyInfo prop = type?.GetProperty("id")!;
        var id = prop.GetValue(entity);

        var filter = Builders<T>.Filter.Eq("id", id);
        return collection.ReplaceOneAsync(filter, entity, new ReplaceOptions
        {
            IsUpsert = true
        });
    }
}