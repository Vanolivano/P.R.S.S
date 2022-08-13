using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publication.Rabbit.Subscription.Storage.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args.Default;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy;

namespace Publication.Rabbit.Subscription.Storage.ConsoleInput
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			using IHost host = Host.CreateDefaultBuilder(args)
				.ConfigureServices((_, services) =>
					services
						.AddHttpClient()
						.AddRmqPublisherClient())
						.Build();

			using IServiceScope serviceScope = host.Services.CreateScope();
			IServiceProvider provider = serviceScope.ServiceProvider;
			var service = provider.GetRequiredService<IRmqPublisherClient>();
			
			
			var result = await service.SendData(new PersonArgs
			{
				Age = 18,
				Gender = Gender.M,
				Name = "Ivan",
				BirthDate = new DateTime(2002, 01, 12)
			}).ConfigureAwait(false);

			Console.WriteLine(result.ErrorData != null
				? $"Error codee: {result.ErrorData.ErrorCode}{Environment.NewLine}Error message: {result.ErrorData.ErrorMessage}."
				: "Person data has been sent successfully.");
		}

		private static string GetName()
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

		private static int GetAge()
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

		private static Gender GetGender()
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

		private static DateTime GetBirtDate()
		{
			while (true)
			{
				Console.WriteLine("Input birth date");
				var birthDate = Console.ReadLine();
				if (DateTime.TryParse(birthDate, out var date))
				{
					return date;
				}

				Console.WriteLine("Incorrect birth date, try again.");
			}
		}
	}
}