namespace Aerariu.API.Lib.Middleware.Response
{
    public class ResponseWithData<T> : IResponseWithData<T>
    {
        public required string Message { get; set; }
        public required int StatusCode { get; set; }
        public required T ResultData { get; set; }
    }
}
