using System.Net.Http.Headers;

namespace Dev.Tools.Helpers
{
	public static class HttpRequestHeadersHelper
	{
		public const string Authorization = "Authorization";
		public const string AuthorizationLogin = "AuthorizationLogin";
		public const string AuthorizationPassword = "AuthorizationPassword";

		public static HttpRequestHeaders AppendAuthorizationHeader(
			this HttpRequestHeaders httpRequestHeaders,
			string sessionIdentifier)
		{
			httpRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionIdentifier);
			return httpRequestHeaders;
		}

		public static HttpRequestHeaders AppendAuthorizationLoginHeader(
			this HttpRequestHeaders httpRequestHeaders,
			string login,
			string password)
		{
			httpRequestHeaders.Add("AuthorizationLogin", login);
			httpRequestHeaders.Add("AuthorizationPassword", password);
			return httpRequestHeaders;
		}
	}
}