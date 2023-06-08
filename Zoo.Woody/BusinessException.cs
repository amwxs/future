namespace Zoo.Woody
{
    public class BusinessException: Exception
    {
        public int Code { get; }
        public BusinessException(int code,string message):base(message)
        {
            Code = code;
        }
    }
}
