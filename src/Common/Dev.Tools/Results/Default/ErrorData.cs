namespace Dev.Tools.Results.Default
{
    public class ErrorData : IErrorData
    {
        public ErrorData(string errorMessage, int errorCode = 400)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }


        public override string ToString()
        {
            return $"{nameof(ErrorCode)}: {ErrorCode}, {nameof(ErrorMessage)}: {ErrorMessage}.";
        }
    }
}