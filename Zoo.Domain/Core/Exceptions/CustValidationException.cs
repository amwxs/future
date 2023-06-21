using Zoo.Domain.Core.Result;

namespace Zoo.Domain.Core.Exceptions;
public class CustValidationException: Exception
{
    public IEnumerable<Error>? ValidationErrors { get; }

    public CustValidationException(IEnumerable<Error> errors)
    {
        ValidationErrors = errors;
    }
}
