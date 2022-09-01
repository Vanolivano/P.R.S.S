using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.Mappers
{
	public static class PersonArgsDtoMapper
	{
		public static PersonDto ToDto(this IPersonArgs personArgs)
		{
			return personArgs is null
				? null
				: new PersonDto
				{
					Name = personArgs.Name,
					Age = personArgs.Age,
					BirthDate = personArgs.BirthDate,
					Gender = personArgs.Gender
				};
		}
	}
}