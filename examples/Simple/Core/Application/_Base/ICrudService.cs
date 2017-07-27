using System;
using Core.Application.Services;

namespace Simple.Application.Services
{
    public interface ICrudService<TEditDto> : ICrudService<Guid, TEditDto>
    {

    }
}