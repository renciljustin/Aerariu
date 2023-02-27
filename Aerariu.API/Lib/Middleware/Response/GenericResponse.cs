namespace Aerariu.API.Lib.Middleware.Response
{
    public class GenericResponse : IGenericResponse
    {
        public required string Message { get; set; }
        public required int StatusCode { get; set; }
    }
}
