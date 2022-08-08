using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Args.Default
{
	public class PersonArgs : IPersonArgs
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public DateOnly BirthDate { get; set; }
	}
}