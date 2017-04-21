using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lemolsoft.Framework.Domain;
using Lemolsoft.Framework.Domain.Data;

namespace Lemolsoft.Framework.Application.Services
{
    public class RetrieveService<TRepository, TEntity> : IRetrieveService
        where TEntity : IEntity
        where TRepository : IRepository<TEntity>
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
        public virtual TDto Find<TDto>(Guid id)
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

    public class RetrieveService<TEntity> : RetrieveService<IRepository<TEntity>, TEntity>
        where TEntity : IEntity
    {
        public RetrieveService(IMapper mapper, IRepository<TEntity> repository)
            : base(mapper, repository)
        {
        }
    }
}