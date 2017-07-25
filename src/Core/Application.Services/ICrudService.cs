namespace Lemolsoft.Framework.Application.Services
{
    public interface ICrudService<TIdentity, TEditDto> : IRetrieveService<TIdentity>, IEditService<TIdentity, TEditDto>
    {
    }
}