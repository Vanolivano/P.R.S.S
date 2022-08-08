namespace Dev.Tools.Errors
{
	public interface IErrorData
	{
		string ErrorMessage { get; }

		int? ErrorCode { get; }
	}
}