using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models
{
	public interface IPerson
	{
		string Name { get; }
		int Age { get; }
		Gender Gender { get; }
		DateTime BirthDate { get; }
	}
}