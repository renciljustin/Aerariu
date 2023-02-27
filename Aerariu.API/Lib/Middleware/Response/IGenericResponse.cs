namespace Aerariu.API.Lib.Middleware.Response
{
    public interface IGenericResponse
    {
        string Message { get; set; }
        int StatusCode { get; set; }
    }
}
