using System;
using Publication.Rabbit.Subscription.Storage.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models.Default
{
    public class Person : IPerson
    {
        public Person(string name, int age, Gender gender, DateTime birthDate)
        {
            Name = name;
            Age = age;
            Gender = gender;
            BirthDate = birthDate;
        }

        public string Name { get; }
        public int Age { get; }
        public Gender Gender { get; }
        public DateTime BirthDate { get; }
    }
}