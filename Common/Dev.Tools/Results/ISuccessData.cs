namespace Dev.Tools.Results
{
	public interface ISuccessData : IResultWithErrorData
	{
		bool Succeeded { get; }
	}
}