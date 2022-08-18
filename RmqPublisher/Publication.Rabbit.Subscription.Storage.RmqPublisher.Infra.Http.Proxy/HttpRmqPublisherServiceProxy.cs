using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy
{
	internal sealed partial class HttpRmqPublisherServiceProxy : IRmqPublisherClient
	{
		public async Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken)
		{
			using var request = CreateHttpRequestMessage(
				HttpMethod.Post,
				GetRmqPublisherUri("send-data"),
				args.ToDto(),
				default);

			var result = await GetSuccessDataByRequest(request, cancellationToken)
				.ConfigureAwait(false);

			return result;
		}
	}
}