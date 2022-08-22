using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy.Mappers
{
	internal static class PersonArgsDtoMapper
	{
		public static PersonDto ToDto(this IPersonArgs personArgs)
		{
			return personArgs is null
				? null
				: new PersonDto
				{
					Name = personArgs.Name,
					Age = personArgs.Age,
					Gender = personArgs.Gender,
					BirthDate = personArgs.BirthDate
				};
		}
	}
}