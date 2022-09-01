using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models
{
	public interface IPerson
	{
		string Name { get; }
		int Age { get; }
		Gender Gender { get; }
		DateTime BirthDate { get; }
	}
}