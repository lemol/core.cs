using System;

namespace Lemolsoft.Framework.Domain
{
    public abstract class EntityBase<TIdentity> : IEntity<TIdentity>
    {
        public virtual TIdentity Id { get; protected set; }
        protected abstract TIdentity GenerateId();
        protected void NewEntity()
        {
            Id = GenerateId();
        }
    }
}