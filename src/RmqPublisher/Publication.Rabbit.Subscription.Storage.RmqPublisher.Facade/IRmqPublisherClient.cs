using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade
{
	public interface IRmqPublisherClient
	{
		public Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken = default);
	}
}

