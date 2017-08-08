namespace Core.Application.WebApi.Controllers
{
    public class SuccessResult<TResult> : ApiResult
    {
        public TResult Data { get; set; }
        public SuccessResult(TResult data, string message = "Retornado com sucesso")
            : base(message)
        {
            Data = data;
        }
    }
}