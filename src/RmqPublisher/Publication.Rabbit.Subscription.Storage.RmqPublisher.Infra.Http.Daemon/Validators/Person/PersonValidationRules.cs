using System;
using FluentValidation;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Validators.Person
{
	public static class PersonValidationRules
	{
		public static IRuleBuilderOptions<T, string> FirstNameValidation<T>(this IRuleBuilder<T, string> rule)
		{
			return rule
				.NotEmpty()
				.MinimumLength(3)
				.MaximumLength(30);
		}

		public static IRuleBuilderOptions<T, int> AgeValidation<T>(this IRuleBuilder<T, int> rule)
		{
			return rule
				.NotEmpty()
				.GreaterThanOrEqualTo(18)
				.LessThanOrEqualTo(150);
		}

		public static IRuleBuilderOptions<T, Gender> GenderValidation<T>(this IRuleBuilder<T, Gender> rule)
		{
			return rule
				.NotEmpty()
				.IsInEnum();
		}

		public static IRuleBuilderOptions<T, DateTime> BirthDateValidation<T>(this IRuleBuilder<T, DateTime> rule)
		{
			return rule
				.NotEmpty()
				.InclusiveBetween(new DateTime(1850, 1, 1), DateTime.Today.AddYears(-18));
		}
	}
}