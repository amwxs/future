using Zoo.Domain.Core.Result;

namespace Zoo.Domain.Core.Exceptions;

public class CustException : Exception
{
    public string Code { get; }
    //public IEnumerable<ValidationError>? ValidationErrors { get; }

    public CustException(string code, string message)
        : base(message)
    {
        Code = code;
        //ValidationErrors = validationErrors;
    }
    public CustException(string code, string message, Exception innerException)
        : base(message, innerException)
    {
        Code = code;
        //ValidationErrors = validationErrors;
    }
}
