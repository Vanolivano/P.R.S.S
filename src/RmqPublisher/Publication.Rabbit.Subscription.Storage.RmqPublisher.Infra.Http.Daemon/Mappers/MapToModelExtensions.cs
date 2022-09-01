using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models.Default;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Mappers
{
	public static class MapToModelExtensions
	{
		public static IPerson FromDto(this PersonDto dto)
		{
			return dto is null
				? null
				: new Person
				{
					Name = dto.Name,
					Age = dto.Age,
					Gender = dto.Gender,
					BirthDate = dto.BirthDate
				};
		}
	}
}