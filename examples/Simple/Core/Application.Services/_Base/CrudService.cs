using System;
using System.Collections.Generic;
using Domain.Data;
using Domain.Model;

namespace Application.Services
{
    public abstract class CrudService<TEntity, TEditDto> : Core.Application.Services.CrudService<TEntity, Guid, TEditDto>, ICrudService<TEditDto>
        where TEntity : IEntity
    {
        protected CrudService(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }

        public Guid Create(Guid dto)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TDto> GetQuery<TDto>(TEditDto query) =>
            throw new NotImplementedException();

        public virtual void Update(Guid id, Guid dto)
        {
            throw new NotImplementedException();
        }

        protected override void DeleteAbstract(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
        }
    }

    public abstract class CrudService<TEntity, TEditDto, TQuery> : Core.Application.Services.CrudService<TEntity, Guid, TEditDto, TQuery>, ICrudService<TEditDto, TQuery>
        where TEntity : IEntity
    {
        protected CrudService(IApplicationMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }

        protected override void DeleteAbstract(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
        }
    }
}