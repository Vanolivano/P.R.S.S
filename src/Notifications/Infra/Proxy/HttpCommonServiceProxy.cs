using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Configs;
using Dev.Tools.Helpers;
using Dev.Tools.Results;
using Dev.Tools.Results.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy
{
	internal sealed partial class HttpNotificationServiceProxy
	{
		private const string MediaType = "application/json";
		private const string RequestPrefix = "api/v1";
        private const string NotificationControllerName = "notifications";

        private string AuthToken { get; }
		private readonly HttpClient _httpClient;
		private readonly ILogger<HttpNotificationServiceProxy> _logger;


		public HttpNotificationServiceProxy(
			IOptions<HttpClientConfig> config,
			IOptions<AuthConfig> authConfig,
			IHttpClientFactory httpClientFactory,
			ILogger<HttpNotificationServiceProxy> logger)
		{
			_httpClient = httpClientFactory.CreateClient(config.Value.HttpClientName);
			AuthToken = authConfig.Value.AuthToken;
			_logger = logger;
		}

		private static HttpRequestMessage CreateHttpRequestMessage(
			HttpMethod method,
			string requestUri,
			string message,
			string authToken)
		{
			var request = new HttpRequestMessage(method, requestUri)
			{
				Content = new StringContent(
					message,
					Encoding.UTF8,
					MediaType)
			};
			request.Headers.AppendAuthorizationHeader(authToken);

			return request;
		}

		private async Task<ISuccessData> GetSuccessDataByRequest(
			HttpRequestMessage request,
			CancellationToken token)
		{
			try
			{
				using var response = await _httpClient.SendAsync(request, token)
					.ConfigureAwait(false);

				if (response.IsSuccessStatusCode)
				{
					return SuccessDataBuilder.BuildSuccess();
				}

				return SuccessDataBuilder.BuildError(await GetErrorDataAsync(response).ConfigureAwait(false));
			}
			catch (Exception exception)
			{
				_logger.LogError($"Error while sending http request. Reason: {exception.Message}.", exception);
				return SuccessDataBuilder.BuildError(exception);
			}
		}

		private static async Task<IErrorData> GetErrorDataAsync(HttpResponseMessage response)
		{
			var errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return ErrorDataBuilder.BuildErrorData(errorMessage, (int) response.StatusCode);
		}

		private static string GetNotificationUri(string operation, Guid? id = null) =>
			GetRequestUri(NotificationControllerName, operation, id);

		private static string GetRequestUri(string controller, string operation, Guid? id)
		{
			var res = UrlHelper.Combine(
				RequestPrefix,
				controller);
			if (id != null)
			{
				res = UrlHelper.Combine(res, id.ToString());
			}

			if (operation != null)
			{
				res = UrlHelper.Combine(res, operation);
			}

			return res;
		}
	}
}