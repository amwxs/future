using Zoo.Domain.Core.Result;

namespace Zoo.Domain.Core.Exceptions;
public class CustValidationException: Exception
{
    public IEnumerable<ValidationError>? ValidationErrors { get; }

    public CustValidationException(IEnumerable<ValidationError> errors)
    {
        ValidationErrors = errors;
    }
}
