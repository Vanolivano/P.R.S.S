using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models.Default;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Mappers;

public static class PersonMapperExtensions
{
    public static IPerson ToModel(this PersonDto dto)
    {
        return new Person(name: dto.Name, age: dto.Age, gender: dto.Gender, birthDate: dto.BirthDate);
    }
}