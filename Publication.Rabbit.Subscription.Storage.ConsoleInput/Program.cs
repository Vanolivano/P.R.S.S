// See https://aka.ms/new-console-template for more information

using System;
using Publication.Rabbit.Subscription.Storage;
using Publication.Rabbit.Subscription.Storage.Models;

Console.WriteLine("Hello, World!");
var person = new Person(GetName(), GetAge(), GetGender(), GetBirtDate());


string GetName()
{
	while (true)
	{
		Console.WriteLine("Input Name");
		var name = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("Incorrect name, try again.");
		}
		else
		{
			return name;
		}
	}
}

int GetAge()
{
	while (true)
	{
		Console.WriteLine("Input age: ");
		var age = Console.ReadLine();
		if (int.TryParse(age, out var ageInt))
		{
			return ageInt;
		}

		Console.WriteLine("Incorrect age, try again.");
	}
}

Gender GetGender()
{
	while (true)
	{
		Console.WriteLine("Input gender, M or F: ");
		var gender = Console.ReadLine();
		if (Enum.TryParse<Gender>(gender, out var result))
		{
			return result;
		}

		Console.WriteLine("Incorrect gender, try again.");
	}
}

DateOnly GetBirtDate()
{
	while (true)
	{
		Console.WriteLine("Input birth date");
		var birthDate = Console.ReadLine();
		if (DateOnly.TryParse(birthDate, out var date))
		{
			return date;
		}

		Console.WriteLine("Incorrect birth date, try again.");
	}
}