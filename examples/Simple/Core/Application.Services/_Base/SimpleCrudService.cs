using System;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Simple.Application.Services
{
    public class SimpleCrudService<TEntity, TEditDto> : Core.Application.Services.SimpleCrudService<TEntity, Guid, TEditDto>
        where TEntity : IEntity
    {
        public SimpleCrudService(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }
}