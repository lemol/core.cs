using System;
using Core.Application.Services;

namespace Application.Services
{
    public interface ICrudService<TEditDto> : ICrudService<Guid, TEditDto>
    {
    }

    public interface ICrudService<TEditDto, TQuery> : ICrudService<Guid, TEditDto, TQuery>
    {
    }
}