using System;

namespace Lemolsoft.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        public virtual Guid Id { get; protected set; }
        protected virtual Guid GenerateId()
        {
            return Guid.NewGuid();
        }
        protected void NewEntity()
        {
            Id = GenerateId();
        }
    }
}