using System;

namespace Core.Domain.Model
{
    public interface IEntity<TIdentity>
    {
        TIdentity Id { get; }
    }
}
