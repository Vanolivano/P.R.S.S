namespace Dev.Tools.Results.Default
{
	public class ResultWithErrorData<TResult> : IResultWithErrorData<TResult>
	{
		public TResult Result { get; set; }
		public IErrorData ErrorData { get; set; }
	}
}