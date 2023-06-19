using Zoo.Application.Core;

namespace Zoo.Domain.Core;

public class BizException : Exception
{
    public string Code { get; }
    public IEnumerable<ValidationError>? ValidationErrors { get; }

    public BizException(string code, string message, IEnumerable<ValidationError>? validationErrors = null)
        : base(message)
    {

        Code = code;
        ValidationErrors = validationErrors;
    }
    public BizException(string code, string message, Exception innerException, IEnumerable<ValidationError>? validationErrors = null)
        : base(message, innerException)
    {
        Code = code;
        ValidationErrors = validationErrors;
    }
}
