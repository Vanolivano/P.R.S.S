using System.Threading.Tasks;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Services
{
	public interface IRmqSubscriberService
	{
		Task SavePersonAsync(IPerson person);
	}
}