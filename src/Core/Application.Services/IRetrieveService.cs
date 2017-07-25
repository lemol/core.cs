using System;
using System.Collections.Generic;

namespace Lemolsoft.Framework.Application.Services
{
    public interface IRetrieveService<TIdentity>
    {
        IEnumerable<TDto> GetAll<TDto>();
        TDto Find<TDto>(TIdentity id);
    }
}