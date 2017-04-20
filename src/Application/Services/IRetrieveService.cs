using System;
using System.Collections.Generic;

namespace Lemolsoft.Framework.Application.Services
{
    public interface IRetrieveService
    {
        IEnumerable<TDto> GetAll<TDto>();
        TDto Find<TDto>(Guid id);
    }
}