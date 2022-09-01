using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args.Default
{
	public class PersonArgs : IPersonArgs
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public DateTime BirthDate { get; set; }
	}
}