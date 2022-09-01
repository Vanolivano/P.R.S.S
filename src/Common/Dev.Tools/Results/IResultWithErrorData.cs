namespace Dev.Tools.Results
{
	public interface IResultWithErrorData
	{
		IErrorData ErrorData { get; }
	}

	public interface IResultWithErrorData<out TResult> : IResultWithErrorData
	{
		TResult Result { get; }
	}
}