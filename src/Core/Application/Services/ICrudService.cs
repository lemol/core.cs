namespace Core.Application.Services
{
    public interface ICrudService<TIdentity, TEditDto, TQuery> : IRetrieveService<TIdentity, TQuery>, IEditService<TIdentity, TEditDto>
    {
    }
}