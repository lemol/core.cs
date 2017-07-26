using System;
using Core.Application.Services;
using Simple.Domain;

namespace Simple.Application.Services
{
    public class CrudService<TEntity, TEditDto> : CrudService<IRepository<TEntity>, Guid, TEditDto>
        where TEntity : IEntity
    {
        protected CrudService(IMapper mapper, IRepository<TEntity> repository)
            : base(mapper, repository)
        {
        }
    }
}