using Dev.Tools.Results;
using Microsoft.AspNetCore.Http;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers.Bearer
{
	public interface IBearerAuthorizer
	{
		public ISuccessData Authorize(HttpRequest httpRequest);
	}
}