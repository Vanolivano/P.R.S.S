using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Errors;
using Dev.Tools.Errors.Default;
using Dev.Tools.Helpers;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy
{
	internal sealed partial class HttpRmqPublisherServiceProxy
	{
		private const string MediaType = "application/json";
		private const string RequestPrefix = "api/v1";

		private readonly string _baseRequestUri;
		private readonly HttpClient _httpClient;

		public HttpRmqPublisherServiceProxy(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_baseRequestUri = "https://localhost:7171";
		}

		private static HttpRequestMessage CreateHttpRequestMessage<T>(
			HttpMethod method,
			string requestUri,
			T dto,
			string sessionIdentifier)
		{
			var request = new HttpRequestMessage(method, requestUri)
			{
				Content = new StringContent(
					JsonSerializer.Serialize(dto),
					Encoding.UTF8,
					MediaType)
			};
			//request.Headers.AppendAuthorizationHeader(sessionIdentifier);

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
					return new SuccessData {Succeeded = true};
				}

				return new SuccessData
				{
					ErrorData = await GetErrorDataAsync(response).ConfigureAwait(false),
					Succeeded = false
				};
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error while sending http request. Reason: {e}.");
				return new SuccessData
				{
					ErrorData = new ErrorData(e.Message, 500)
				};
			}
		}

		private static async Task<ErrorData> GetErrorDataAsync(HttpResponseMessage response)
		{
			var errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return new ErrorData
			{
				ErrorCode = (int) response.StatusCode,
				ErrorMessage = errorMessage,
			};
		}

		private string GetRmqPublisherUri(string operation, Guid? id = null) =>
			GetRequestUri("rmq-publisher", operation, id);

		private string GetRequestUri(string controller, string operation, Guid? id)
		{
			var res = UrlHelper.Combine(
				_baseRequestUri,
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