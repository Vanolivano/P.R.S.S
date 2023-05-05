using System.Threading.Tasks;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Services;
using Microsoft.Extensions.Logging;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.BL;

public class RmqSubscriberService : IRmqSubscriberService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<RmqSubscriberService> _logger;

    public RmqSubscriberService(IPersonRepository personRepository, ILogger<RmqSubscriberService> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public Task SavePersonAsync(IPerson person)
    {
        _logger.LogInformation($"{nameof(SavePersonAsync)} has started.");
        return _personRepository.CreateAsync(person);
    }
}
