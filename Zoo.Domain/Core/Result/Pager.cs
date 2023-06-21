namespace Zoo.Domain.Core.Result;

public class Pager
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex * PageSize < TotalCount;

    public Pager(int pageNo, int pageSize, int totalCount)
    {
        PageIndex = pageNo;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
}
