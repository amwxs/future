namespace Zoo.Application.Core.Primitives;


public class Result
{
    public static Result<T> Success<T>(T data, Pager? pager = null)
    {
        var response = new Result<T> { Data = data, Pager = pager, };
        return response;
    }

    public static Result<T> Failure<T>(string code, string message, IEnumerable<Error>? errors=null)
    {
        var response = new Result<T> { Code = code, Message = message, Errors = errors };
        return response;
    }
}

public class Result<T> : Result
{
    public string Code { get; set; } = "0";
    public string? Message { get; set; }
    public T? Data { get; set; }
    public Pager? Pager { get; set; }
    public IEnumerable<Error>? Errors { get; set; }
}

public class Pager
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }

    public Pager(int pageNo, int pageSize, int total)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        Total = total;
    }
}
public class Error
{
    public string Filed { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}