using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Repositories;

public class MongoRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public Task CreateAsync(T entity)
    {
        return _collection.InsertOneAsync(entity);
    }

    public async Task<ICollection<T>> GetAllAsync()
    {
        var entities = await _collection.FindAsync(FilterDefinition<T>.Empty);
        return await entities.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var entity = await _collection.FindAsync(filter);
        return await entity.FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(string id, T entity)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
    }

     public async Task<ICollection<T>> GetByFilterAsync(string filter)
    {
        var bsonFilter = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(filter);
        var entities = await _collection.FindAsync(bsonFilter);
        return await entities.ToListAsync();
    }
}
