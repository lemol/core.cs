namespace Core.Application.WebApi.Controllers
{
    public abstract class ApiResult
    {
        public string Message { get; set; }
        public ApiResult(string message)
        {
            Message = message;
        }
    }
}