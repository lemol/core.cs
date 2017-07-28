using System;
using Simple.Application;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Application.WebApi.Controllers
{
    public class SimpleCrudController<TEntity, TDto> : Core.Application.WebApi.Controllers.SimpleCrudController<TEntity, Guid, TDto>
        where TEntity : IEntity
    {
        public SimpleCrudController(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }
}