using Dev.Tools.Configs;
using Dev.Tools.Results;
using Dev.Tools.Results.Builders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers.Bearer
{
	public class BearerAuthorizer : IBearerAuthorizer
	{
		private readonly AuthConfig _authConfig;

		public BearerAuthorizer(IOptions<AuthConfig> authConfigOptions)
		{
			_authConfig = authConfigOptions.Value;
		}

		public ISuccessData Authorize(HttpRequest httpRequest)
		{
			var authToken = (string) httpRequest?.Headers?.Authorization;

			if (string.IsNullOrWhiteSpace(authToken)
			    || authToken.StartsWith("Bearer ") == false
			    || authToken.Contains(_authConfig.AuthToken) == false)
			{
				return SuccessDataBuilder.BuildError("Authorization failed", 400);
			}

			return SuccessDataBuilder.BuildSuccess();
		}
	}
}