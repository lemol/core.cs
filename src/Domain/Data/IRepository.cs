using System;
using System.Linq;

namespace Lemolsoft.Framework.Domain.Data
{
    public interface IRepository<T>  where T : IEntity
    {
        T Find(Guid id);
        IQueryable<T> Query();
        void Create(T item);
        void Update(T item);
    }
}