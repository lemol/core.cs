using System;
using Core.Application.Services;
using Core.Infrastructure;
using Simple.Domain;

namespace Simple.Application.Services
{
    public abstract class CrudService<TEntity, TEditDto> : CrudService<TEntity, Guid, TEditDto>
        where TEntity : IEntity
    {
        protected CrudService(IMapper mapper, IRepository<TEntity> repository)
            : base(mapper, repository)
        {
        }

        protected override void DeleteAbstract(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
        }
    }
}