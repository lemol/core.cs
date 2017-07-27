using System;

namespace Core.Domain.Model
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