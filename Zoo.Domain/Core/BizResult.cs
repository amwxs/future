namespace Zoo.Application.Core;


public class BizResult
{

    public static BizResult<T> Success<T>(T data, Pager? pager = null)
    {
        var response = new BizResult<T> { Data = data, Pager = pager, };
        return response;
    }

    public static BizResult<T> Failure<T>(string code, string message, IEnumerable<ValidationError>? validationErrors)
    {
        var response = new BizResult<T> { Code = code, Message = message, ValidationErrors = validationErrors};
        return response;
    }

}

public class BizResult<T> : BizResult
{
    private const string _defualtOk = "0";
    public string Code { get; set; } = _defualtOk;

    public string? Message { get; set; }

    public IEnumerable<ValidationError>? ValidationErrors { get; set; }

    public T? Data { get; set; }

    public Pager? Pager { get; set; }
}

public class Pager
{
    public int PageNo { get; }
    public int PageSize { get; }
    public int Total { get; }

    public Pager(int pageNo, int pageSize, int total)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        Total = total;
    }
}

public class ValidationError
{
    public string Filed { get;}
    public string Message { get;}

    public ValidationError(string filed,string message)
    {
        Filed = filed;
        Message = message;
    }
}