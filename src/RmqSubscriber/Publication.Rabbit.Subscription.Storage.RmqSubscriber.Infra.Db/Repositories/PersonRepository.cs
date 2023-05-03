using System.Collections.Generic;
using System.Threading.Tasks;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Mappers;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Repositories;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db;

public class PersonRepository : IPersonRepository
{
    private readonly MongoRepository<PersonModel> _mongoRepository;
    public PersonRepository()
    {
        _mongoRepository = new MongoRepository<PersonModel>(
            "mongodb://root:rootpassword@127.0.0.1:27017/",
             "databasename",
              "personCollectionName");
    }

    public Task CreateAsync(IPerson entity)
    {
        return _mongoRepository.CreateAsync(entity.ToDbModel());
    }

    public Task DeleteAsync(string id)
    {
        return _mongoRepository.DeleteAsync(id);
    }

    public Task<ICollection<IPerson>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<ICollection<IPerson>> GetByFilterAsync(string filter)
    {
        throw new System.NotImplementedException();
    }

    public Task<IPerson> GetByIdAsync(string id)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(string id, IPerson entity)
    {
        throw new System.NotImplementedException();
    }
}