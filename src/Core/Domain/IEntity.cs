using System;

namespace Core.Domain
{
    public interface IEntity<TIdentity>
    {
        TIdentity Id { get; }
    }
}
