using System.Collections.Generic;
using System.Threading.Tasks;
using Dev.Tools.Configs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Mappers;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Repositories;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db;

public class PersonRepository : IPersonRepository
{
    private const string DatabaseName = "databasename";
    private const string CollectionName = "PersonModels";
    private readonly MongoRepository<PersonModel> _mongoRepository;
    private readonly ILogger<PersonRepository> _logger;
    public PersonRepository(IOptions<MongoDbConfig> mongoDbConfig, ILogger<PersonRepository> logger)
    {
        _mongoRepository = new MongoRepository<PersonModel>(
            mongoDbConfig.Value.ConnectionString,
            DatabaseName,
            CollectionName);
        _logger = logger;
    }

    public Task CreateAsync(IPerson entity)
    {
        _logger.LogInformation($"{nameof(CreateAsync)} has started.");
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