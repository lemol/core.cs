using System;
using Simple.Domain.Model;

namespace Simple.Domain.Data
{
    public interface IRepository<TEntity> : Core.Domain.Data.IRepository<TEntity, Guid>
        where TEntity : IEntity
    {
    }
}