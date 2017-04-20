namespace Lemolsoft.Framework.Application.Services
{
    public interface ICrudService<TEditDto> : IRetrieveService, IEditService<TEditDto>
    {
    }
}