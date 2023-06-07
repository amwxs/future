namespace Zoo.Woody
{

    public class ApiResponse
    {
        public static ApiResponse<T> Success<T>(T data, Pager? pager= null)
        {
            var response = new ApiResponse<T> {Data = data, Pager = pager, };
            return response;
        }

        public static ApiResponse<T> Failure<T>(int code, string message)
        {
            var response = new ApiResponse<T> {Code = code, Message = message};
            return response;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public Pager? Pager { get; set; }
    }

    public class Pager
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public Pager()
        {
            
        }
        public Pager(int pageNo,int pageSize,int total)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            Total = total;
        }
    }
}