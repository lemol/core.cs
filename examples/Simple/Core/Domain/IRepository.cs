using System;
using Core.Domain;
using Core.Domain.Data;

namespace Simple.Domain
{
    public interface IRepository<TEntity> : IRepository<TEntity, Guid>
        where TEntity : IEntity<Guid>
    {
    }
}