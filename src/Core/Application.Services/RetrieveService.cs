using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain;
using Core.Domain.Data;
using Core.Infrastructure;

namespace Core.Application.Services
{
    public class RetrieveService<TRepository, TEntity, TIdentity> : IRetrieveService<TIdentity>
        where TEntity : IEntity<TIdentity>
        where TRepository : IRepository<TEntity, TIdentity>
    {
        #region Fields
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        #endregion

        #region Constructors
        public RetrieveService(IMapper mapper, TRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        #endregion

        #region IRetrieveService
        public virtual TDto Find<TDto>(TIdentity id)
        {
            var item = _repository.Find(id);
            var itemDto = _mapper.Map<TEntity, TDto>(item);
            return itemDto;
        }

        public virtual IEnumerable<TDto> GetAll<TDto>()
        {
            var items = _repository.Query(GetAllIncludes.ToArray());
            var itemsDto = _mapper.Map<TEntity, TDto>(items);
            return itemsDto;
        }
        #endregion

        #region Virtuais
        internal IEnumerable<Expression<Func<TEntity, object>>> GetAllIncludes
        {
            get; set;
        }

        internal IEnumerable<Expression<Func<TEntity, object>>> FindIncludes
        {
            get; set;
        }
        #endregion
    }

    public class RetrieveService<TEntity, TIdentity> : RetrieveService<IRepository<TEntity, TIdentity>, TEntity, TIdentity>
        where TEntity : IEntity<TIdentity>
    {
        public RetrieveService(IMapper mapper, IRepository<TEntity, TIdentity> repository)
            : base(mapper, repository)
        {
        }
    }
}