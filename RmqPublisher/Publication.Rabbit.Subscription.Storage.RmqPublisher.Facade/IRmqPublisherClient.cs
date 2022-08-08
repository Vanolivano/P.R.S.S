using System.Threading;
using System.Threading.Tasks;
using Dev.Tools;
using Dev.Tools.Errors;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher
{
	public interface IRmqPublisherClient
	{
		public Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken = default);
	}
}

