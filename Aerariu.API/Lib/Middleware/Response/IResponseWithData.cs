namespace Aerariu.API.Lib.Middleware.Response
{
    public interface IResponseWithData<T> : IGenericResponse
    {
        T ResultData { get; set; }
    }
}
