using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage
{
	public class Person
	{
		public Person(string name, int age, Gender gender, DateOnly birthDate)
		{
			Name = name;
			Age = age;
			Gender = gender;
			BirthDate = birthDate;
		}

		public Person()
		{
		}

		public string Name { get; set; }
		public int Age { get; set; }
		public Gender Gender { get; set; }
		public DateOnly BirthDate { get; set; }
	}
}