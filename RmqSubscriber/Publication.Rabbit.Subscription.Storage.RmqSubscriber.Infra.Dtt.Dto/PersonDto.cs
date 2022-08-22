using System;
using Dev.Tools.Transport.Dto;
using Publication.Rabbit.Subscription.Storage.Models;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto
{
	public class PersonDto : IPersonArgs, IPostDto
	{
		public string PostKey => DtoConstants.PostKey;
		public string Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public DateTime BirthDate { get; set; }
		
		public override string ToString()
		{
			return $"Name: {Name}, Age: {Age}, Gender: {Gender.ToString()}, BirthDate: {BirthDate}";
		}
	}
}