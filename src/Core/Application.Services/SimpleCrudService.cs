using System;
using Core.Domain.Model;
using Core.Domain.Data;
using Core.Infrastructure;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Core.Application.Services
{
    public class SimpleCrudService<TEntity, TIdentity, TEditDto> : CrudService<TEntity, TIdentity, TEditDto>
        where TEntity : IEntity<TIdentity>
    {
        private readonly IEnumerable<Expression<Func<TEntity, object>>> _includes;

        public SimpleCrudService(
            IMapper mapper
            , IUnitOfWork unitOfWork
            , IRepository<TEntity, TIdentity> repository
            , IEnumerable<Expression<Func<TEntity, object>>> includes = null)
            : base(mapper, unitOfWork, repository)
        {
            _includes = includes;
        }

        protected override TIdentity CreateAbstract(TEditDto dto)
        {
            var entity = _mapper.Map<TEditDto, TEntity>(dto);
            _repository.Create(entity);
            _unitOfWork.Commit();
            return entity.Id;
        }

        protected override void UpdateAbstract(TIdentity id, TEditDto dto)
        {
            var entity = _mapper.Map<TEditDto, TEntity>(dto);
            _repository.Update(entity);
            _unitOfWork.Commit();
        }

        protected override IEnumerable<Expression<Func<TEntity, object>>> FindIncludes
        {
            get
            {
                return _includes ?? Enumerable.Empty<Expression<Func<TEntity, object>>>();
            }
        }
    }
}