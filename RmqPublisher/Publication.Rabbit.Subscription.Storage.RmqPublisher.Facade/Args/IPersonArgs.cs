using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Args
{
	public interface IPersonArgs
	{
		string Name { get; }
		int Age { get; }
		Gender Gender { get; }
		DateOnly BirthDate { get; }
	}
}