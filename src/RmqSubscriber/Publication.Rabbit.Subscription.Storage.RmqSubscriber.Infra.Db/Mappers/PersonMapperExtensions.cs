using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.Mappers;

public static class PersonMapperExtensions
{
    public static PersonModel ToDbModel(this IPerson person)
    {
        return new PersonModel
        {
            Name = person.Name,
            Age = person.Age,
            BirthDate = person.BirthDate,
            Gender = person.Gender
        };
    }
}