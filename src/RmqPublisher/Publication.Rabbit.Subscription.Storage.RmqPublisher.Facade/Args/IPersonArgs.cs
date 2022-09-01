using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args
{
	public interface IPersonArgs
	{
		string Name { get; }
		int Age { get; }
		Gender Gender { get; }
		DateTime BirthDate { get; }
	}
}