using System;
using Simple.Domain.Data;
using Simple.Domain.Model;

namespace Simple.Application.Services
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