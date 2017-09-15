using System;
using System.Linq;
using Domain.Model;

namespace Domain.Data
{
    public interface IRepository<TEntity> : Core.Domain.Data.IRepository<TEntity, Guid>
        where TEntity : IEntity
    {
    }
}