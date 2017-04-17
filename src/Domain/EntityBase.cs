using System;

namespace Lemolsoft.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        public virtual Guid Id { get; protected set; }
    }
}