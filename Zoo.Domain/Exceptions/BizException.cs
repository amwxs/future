namespace Zoo.Domain.Exceptions
{
    public class BizException : Exception
    {
        public string Code { get; }

        public BizException(string code, string message) : base(message)
        {
            Code = code;
        }
        public BizException(string code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }
    }
}
