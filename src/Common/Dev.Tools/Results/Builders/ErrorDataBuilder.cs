using System;
using Dev.Tools.Results.Default;

namespace Dev.Tools.Results.Builders
{
	public static class ErrorDataBuilder
	{
		public static IErrorData BuildErrorData(string message, int code)
		{
			return new ErrorData {ErrorMessage = message, ErrorCode = code};
		}

		public static IErrorData BuildErrorData(Exception exception)
		{
			return new ErrorData {ErrorMessage = exception.Message, ErrorCode = 500};
		}
	}
}