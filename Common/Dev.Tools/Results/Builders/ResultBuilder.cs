using System;
using Dev.Tools.Results.Default;

namespace Dev.Tools.Results.Builders
{
	public static class ResultBuilder
	{
		public static IResultWithErrorData<T> Build<T>(T result)
		{
			return new ResultWithErrorData<T>
			{
				Result = result
			};
		}

		public static IResultWithErrorData<T> BuildError<T>(string message, int code)
		{
			return new ResultWithErrorData<T>
			{
				ErrorData = ErrorDataBuilder.BuildErrorData(message, code)
			};
		}

		public static IResultWithErrorData<T> BuildError<T>(Exception exception)
		{
			return new ResultWithErrorData<T>
			{
				ErrorData = ErrorDataBuilder.BuildErrorData(exception)
			};
		}
	}
}