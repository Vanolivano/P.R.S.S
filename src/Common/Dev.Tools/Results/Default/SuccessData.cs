namespace Dev.Tools.Results.Default
{
	public class SuccessData : ISuccessData, IResultWithErrorData
	{
		public bool Succeeded { get; set; }

		public IErrorData ErrorData { get; set; }
	}
}