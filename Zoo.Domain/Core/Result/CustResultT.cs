namespace Zoo.Domain.Core.Result;
public class CustResult<T> : CustResult
{
    public T? Data { get; set; }
    public Pager? Pager { get; set; }
}