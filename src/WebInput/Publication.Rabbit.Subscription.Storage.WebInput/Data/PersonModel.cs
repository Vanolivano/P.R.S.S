using System;
using System.ComponentModel.DataAnnotations;
using Publication.Rabbit.Subscription.Storage.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.WebInput.Data
{
	public class PersonModel : IPersonArgs
	{
		[Required]
		[StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Name is too long.")]
		public string Name { get; set; }

		[Required]
		[Range(18, 150, ErrorMessage = "Age invalid (18-150).")]
		public int Age { get; set; }

		[Required]
		public Gender Gender { get; set; }

		[Required] 
		public DateTime BirthDate { get; set; }
	}
}