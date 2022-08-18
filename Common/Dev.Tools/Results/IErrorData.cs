namespace Dev.Tools.Results
{
	public interface IErrorData
	{
		string ErrorMessage { get; }

		int ErrorCode { get; }
	}
}