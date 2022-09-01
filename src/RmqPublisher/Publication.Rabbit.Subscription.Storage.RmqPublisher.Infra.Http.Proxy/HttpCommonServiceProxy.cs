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

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy
{
	internal sealed partial class HttpRmqPublisherServiceProxy
	{
		private const string MediaType = "application/json";
		private const string RequestPrefix = "api/v1";
		private string AuthToken { get; }
		private readonly HttpClient _httpClient;
		private readonly ILogger<HttpRmqPublisherServiceProxy> _logger;


		public HttpRmqPublisherServiceProxy(
			IOptions<HttpClientConfig> config,
			IOptions<AuthConfig> authConfig,
			IHttpClientFactory httpClientFactory,
			ILogger<HttpRmqPublisherServiceProxy> logger)
		{
			_httpClient = httpClientFactory.CreateClient(config.Value.HttpClientName);
			AuthToken = authConfig.Value.AuthToken;
			_logger = logger;
		}

		private static HttpRequestMessage CreateHttpRequestMessage<T>(
			HttpMethod method,
			string requestUri,
			T dto,
			string authToken)
		{
			var request = new HttpRequestMessage(method, requestUri)
			{
				Content = new StringContent(
					JsonSerializer.Serialize(dto),
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

		private static string GetRmqPublisherUri(string operation, Guid? id = null) =>
			GetRequestUri("rmq-publisher", operation, id);

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