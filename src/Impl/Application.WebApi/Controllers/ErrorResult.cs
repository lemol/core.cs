namespace Core.Application.WebApi.Controllers
{
    public class ErrorResult : ApiResult
    {
        public string Error { get; set; }
        public ErrorResult(string error, string message)
            : base(message)
        {
            Error = error;
        }
    }
}