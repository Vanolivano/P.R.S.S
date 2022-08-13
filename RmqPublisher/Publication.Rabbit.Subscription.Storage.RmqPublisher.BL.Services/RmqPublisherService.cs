using Dev.Tools.Errors;
using Dev.Tools.Errors.Default;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Models;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.BL.Services
{
	public class RmqPublisherService : IRmqPublisherService
	{
		public ISuccessData SendData(IPerson person)
		{
			// return new SuccessData {Succeeded = true};
			throw new System.NotImplementedException();
		}
	}
}

