using System;
using System.Text;
using Dev.Tools.Errors;
using Dev.Tools.Errors.Default;
using Newtonsoft.Json;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args;
using RabbitMQ.Client;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy
{
	public class DttRmqSubscriberServiceProxy : IRmqSubscriberClient
	{
		private const string ExchangeName = "amq.direct";

		public ISuccessData SendData(IPersonArgs personArgs)
		{
			try
			{
				var eventName = personArgs.GetType().Name;
				var factory = new ConnectionFactory
					{HostName = "localhost", Password = "guest", Port = 5672, UserName = "guest"};
				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();
				// channel.ExchangeDeclare(exchange:ExchangeName,
				// 	type: "direct");
				string message = JsonConvert.SerializeObject(personArgs);
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(exchange: ExchangeName,
					routingKey: eventName,
					basicProperties: null,
					body: body);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return new SuccessData
				{
					ErrorData = new ErrorData(e.Message, 500)
				};
			}

			return new SuccessData {Succeeded = true};
		}
	}
}