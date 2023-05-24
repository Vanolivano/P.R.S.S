using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args.Default;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.BL.Services;

public class RmqPublisherService : IRmqPublisherService
{
    private readonly IRmqSubscriberClient _subscriberClient;

    public RmqPublisherService(IRmqSubscriberClient subscriberClient)
    {
        _subscriberClient = subscriberClient;
    }

    public ISuccessData SendData(IPerson person)
    {
        return _subscriberClient.SendData(new PersonArgs
        {
            Name = person.Name,
            Age = person.Age,
            Gender = person.Gender,
            BirthDate = person.BirthDate
        });
    }
}