using System;
using Core.Application.Services;
using Core.Domain.Data;
using Core.Infrastructure;
using Simple.Domain;

namespace Simple.Application.Services
{
    public class RetrieveService<TEntity> : RetrieveService<TEntity, Guid>
        where TEntity : IEntity
    {
        public RetrieveService(IMapper mapper, IRepository<TEntity, Guid> repository)
            : base(mapper, repository)
        {
        }
    }
}