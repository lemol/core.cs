using System;
using System.Collections.Generic;

namespace Core.Application.Services
{
    public interface IRetrieveService<TIdentity>
    {
        IEnumerable<TDto> GetAll<TDto>();
        TDto Find<TDto>(TIdentity id);
    }
}