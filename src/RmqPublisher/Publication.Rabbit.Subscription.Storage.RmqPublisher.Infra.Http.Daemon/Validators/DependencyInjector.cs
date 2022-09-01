using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Validators.Person;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Validators
{
	public static class DependencyInjector
	{
		public static IServiceCollection AddValidation(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IValidator<PersonDto>, PersonDtoValidator>();
	}
}