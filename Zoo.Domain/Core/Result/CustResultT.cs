namespace Zoo.Domain.Core.Result;
public class CustResult<T> : CustResult
{
    private const string _defualtOk = "0";
    public string Code { get; set; } = _defualtOk;

    public string? Message { get; set; }

    public IEnumerable<ValidationError>? ValidationErrors { get; set; }

    public T? Data { get; set; }

    public Pager? Pager { get; set; }

    public static implicit operator CustResult<T>(T value)
    {
        return new CustResult<T>
        {
            Data = value
        };
    }

    public static implicit operator T?(CustResult<T> bizResult)
    {
        return bizResult.Data;
    }

}