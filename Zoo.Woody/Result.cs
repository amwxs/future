namespace Zoo.Woody
{

    public class Result
    {
        public static Result<T> Success<T>(T data, Pager? pager= null)
        {
            var response = new Result<T> {Data = data, Pager = pager, };
            return response;
        }

        public static Result<T> Failure<T>(string code, string message)
        {
            var response = new Result<T> {Code = code, Message = message};
            return response;
        }
    }

    public class Result<T> : Result
    {
        public string Code { get; set; } = "0";
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public Pager? Pager { get; set; }
    }

    public class Pager
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public Pager(int pageNo,int pageSize,int total)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            Total = total;
        }
    }
}