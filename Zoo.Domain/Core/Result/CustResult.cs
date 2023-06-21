namespace Zoo.Domain.Core.Result;


public class CustResult
{

    public static CustResult<T> Success<T>(T data, Pager? pager = null)
    {
        var response = new CustResult<T> { Data = data, Pager = pager, };
        return response;
    }
    public static CustResult<T> Failure<T>(string code, string message, IEnumerable<ValidationError>? validationErrors)
    {
        var response = new CustResult<T> { Code = code, Message = message, ValidationErrors = validationErrors };
        return response;
    }
}
