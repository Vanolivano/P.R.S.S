using System.Threading.Tasks;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Services;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.BL;

public class RmqSubscriberService : IRmqSubscriberService
{
    private readonly IPersonRepository _personRepository;

    public RmqSubscriberService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task SavePersonAsync(IPerson person)
    {
        return _personRepository.CreateAsync(person);
    }
}
