using System;
using Core.Domain;

namespace Simple.Domain
{
    public interface IEntity : IEntity<Guid>
    {
    }
}