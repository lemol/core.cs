using System;
using Core.Domain;

namespace Simple.Domain
{
    public class EntityBase : EntityBase<Guid>, IEntity
    {
        protected override Guid GenerateId() => Guid.NewGuid();
    }
}