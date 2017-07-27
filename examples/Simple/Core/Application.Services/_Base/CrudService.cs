using System;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Simple.Application.Services
{
    public abstract class CrudService<TEntity, TEditDto> : Core.Application.Services.CrudService<TEntity, Guid, TEditDto>, ICrudService<TEditDto>
        where TEntity : IEntity
    {
        protected CrudService(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }

        protected override void DeleteAbstract(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
        }
    }
}