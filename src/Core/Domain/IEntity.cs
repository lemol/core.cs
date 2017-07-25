using System;

namespace Lemolsoft.Framework.Domain
{
    public interface IEntity<TIdentity>
    {
        TIdentity Id { get; }
    }
}
