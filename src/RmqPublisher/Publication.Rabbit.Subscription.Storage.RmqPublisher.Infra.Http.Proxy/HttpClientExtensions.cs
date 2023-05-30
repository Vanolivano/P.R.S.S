using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Helpers;
using Dev.Tools.Results;
using Dev.Tools.Results.Builders;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy;

public static class HttpClientExtensions
{
    private const string MediaType = "application/json";
    private const string RequestPrefix = "api/v1";


    public static async Task<ISuccessData> SendPostAsync<T>(
        this HttpClient client,
        T dto,
        string controllerName,
        string methodName,
        string authToken,
        CancellationToken token = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, GetRequestUri(controllerName, methodName))
        {
            Content = new StringContent(
                    JsonSerializer.Serialize(dto),
                    Encoding.UTF8,
                    MediaType)
        };
        request.Headers.AppendAuthorizationHeader(authToken);


        try
        {
            using var response = await client.SendAsync(request, token)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return SuccessDataBuilder.BuildSuccess();
            }

            return SuccessDataBuilder.BuildError(await GetErrorDataAsync(response).ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            return SuccessDataBuilder.BuildError(exception);
        }
    }

    private static string GetRequestUri(string controller, string operation, Guid? id = null)
    {
        var res = UrlHelper.Combine(RequestPrefix, controller);

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

    private static async Task<IErrorData> GetErrorDataAsync(HttpResponseMessage response)
    {
        var errorMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return ErrorDataBuilder.BuildErrorData(errorMessage, (int)response.StatusCode);
    }
}
