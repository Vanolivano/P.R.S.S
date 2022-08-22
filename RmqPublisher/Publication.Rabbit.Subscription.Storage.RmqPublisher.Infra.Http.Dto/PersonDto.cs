using System;
using Publication.Rabbit.Subscription.Storage.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto
{
	public class PersonDto : IPersonArgs
	{
		public override string ToString()
		{
			return $"Name: {Name}, Age: {Age}, Gender: {Gender.ToString()}, BirthDate: {BirthDate}";
		}

		public string Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public DateTime BirthDate { get; set; }
	}
}