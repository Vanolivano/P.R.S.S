using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services
{
	public interface IRmqPublisherService
	{
		ISuccessData SendData(IPerson person);
	}
}

