using System;
using Core.Domain.Model;

namespace Simple.Domain.Model
{
    public class EntityBase : EntityBase<Guid>, IEntity
    {
        protected override Guid GenerateId() => Guid.NewGuid();
    }
}