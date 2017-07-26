using System;
using Core.Domain;

namespace Simple.Domain
{
    public class EntityBase : EntityBase<Guid>
    {
        protected override Guid GenerateId() => Guid.NewGuid();
    }
}