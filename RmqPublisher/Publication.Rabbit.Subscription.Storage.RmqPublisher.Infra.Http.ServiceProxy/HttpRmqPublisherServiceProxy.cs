using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools;
using Dev.Tools.Errors;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Args;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.ServiceProxy
{
	internal sealed partial class HttpRmqPublisherServiceProxy : IRmqPublisherClient
	{
		public Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken)
		{
			using var request = CreateHttpRequestMessage(
				HttpMethod.Post,
				GetRmqPublisherUri("publish"),
				args,
				default);

			var result = GetSuccessDataByRequest(request, cancellationToken)
				.ConfigureAwait(false);

			return result;
		}
	}
}