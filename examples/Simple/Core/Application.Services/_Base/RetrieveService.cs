using System;
using Domain.Data;
using Domain.Model;

namespace Application.Services
{
    public class RetrieveService<TEntity> : Core.Application.Services.RetrieveService<TEntity, Guid>
        where TEntity : IEntity
    {
        public RetrieveService(IApplicationMapper mapper, IRepository<TEntity> repository)
            : base(mapper, repository)
        {
        }
    }
}