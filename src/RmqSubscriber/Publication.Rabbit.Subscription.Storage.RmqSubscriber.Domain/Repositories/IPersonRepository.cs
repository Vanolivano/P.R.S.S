using System.Collections.Generic;
using System.Threading.Tasks;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;

public interface IPersonRepository
{
    Task CreateAsync(IPerson entity);
    Task<ICollection<IPerson>> GetAllAsync();
    Task<IPerson> GetByIdAsync(string id);
    Task UpdateAsync(string id, IPerson entity);
    Task DeleteAsync(string id);
    Task<ICollection<IPerson>> GetByFilterAsync(string filter);
}
