using FluentValidation;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Validators.Person
{
	public class PersonDtoValidator : AbstractValidator<PersonDto>
	{
		public PersonDtoValidator()
		{
			RuleFor(x => x).NotNull();
			RuleFor(x => x.Name).FirstNameValidation();
			RuleFor(x => x.Age).AgeValidation();
			RuleFor(x => x.Gender).GenderValidation();
			RuleFor(x => x.BirthDate).BirthDateValidation();
		}
	}
}