using System;
using Core.Domain.Model;

namespace Domain.Model
{
    public class EntityBase : EntityBase<Guid>, IEntity
    {
        protected override Guid GenerateId() => Guid.NewGuid();
    }
}