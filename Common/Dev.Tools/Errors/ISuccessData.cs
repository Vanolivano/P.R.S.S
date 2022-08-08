namespace Dev.Tools.Errors
{
	public interface ISuccessData : IResultWithErrorData
	{
		bool Succeeded { get; }
	}
}