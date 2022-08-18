using System;
using Dev.Tools.Results.Default;

namespace Dev.Tools.Results.Builders
{
	public static class SuccessDataBuilder
	{
		public static ISuccessData BuildSuccess()
		{
			return new SuccessData {Succeeded = true};
		}

		public static ISuccessData BuildError(IErrorData data)
		{
			return new SuccessData {Succeeded = false, ErrorData = data};
		}

		public static ISuccessData BuildError(Exception data)
		{
			return new SuccessData {Succeeded = false, ErrorData = ErrorDataBuilder.BuildErrorData(data)};
		}

		public static ISuccessData BuildError(string message, int code)
		{
			return new SuccessData {Succeeded = false, ErrorData = ErrorDataBuilder.BuildErrorData(message, code)};
		}
	}
}