namespace Dev.Tools.Errors.Default
{
	public class ErrorData : IErrorData
	{
		public ErrorData(string errorMessage, int errorCode)
		{
			ErrorMessage = errorMessage;
			ErrorCode = errorCode;
		}

		public ErrorData()
		{
		}

		public string ErrorMessage { get; set; }
		public int ErrorCode { get; set; }
	}
}