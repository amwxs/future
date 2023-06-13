namespace Zoo.Application.Core;


public class BizResult
{
    public static BizResult<T> Success<T>(T data, Pager? pager = null)
    {
        var response = new BizResult<T> { Data = data, Pager = pager, };
        return response;
    }

    public static BizResult<T> Failure<T>(string code, string message, IEnumerable<BizError>? errors = null)
    {
        var response = new BizResult<T> { Code = code, Message = message, Errors = errors };
        return response;
    }
}

public class BizResult<T> : BizResult
{
    public string Code { get; set; } = "0";
    public string? Message { get; set; }
    public T? Data { get; set; }
    public Pager? Pager { get; set; }
    public IEnumerable<BizError>? Errors { get; set; }
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
public class BizError
{
    public string Filed { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}