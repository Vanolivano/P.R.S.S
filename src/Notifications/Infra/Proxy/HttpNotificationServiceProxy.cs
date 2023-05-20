using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Results;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy
{
    internal sealed partial class HttpNotificationServiceProxy : INotificationService
    {
        public async Task<ISuccessData> PushMessageAsync(string message, CancellationToken cancellationToken)
        {
            const string methodName = "push-message";
            using var request = CreateHttpRequestMessage(
                HttpMethod.Post,
                GetNotificationUri(methodName),
                message,
                AuthToken);

            var result = await GetSuccessDataByRequest(request, cancellationToken).ConfigureAwait(false);

            return result;
        }
    }
}