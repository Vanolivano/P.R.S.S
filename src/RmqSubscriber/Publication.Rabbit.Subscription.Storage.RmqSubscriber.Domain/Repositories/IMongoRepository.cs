using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;

public interface IMongoRepository<T> where T : class
{
    Task CreateAsync(T entity);
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
    Task<ICollection<T>> GetByFilterAsync(string filter);
}
