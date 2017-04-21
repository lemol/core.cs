using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lemolsoft.Framework.Domain;
using Lemolsoft.Framework.Domain.Data;

namespace Lemolsoft.Framework.Application.Services
{
    public abstract class CrudService<TEntity, TEditDto> : CrudService<IRepository<TEntity>, TEntity, TEditDto>
        where TEntity : IEntity
    {
        protected CrudService(IMapper mapper, IRepository<TEntity> repository)
            : base(mapper, repository)
        {
        }
    }

    public abstract class CrudService<TRepository, TEntity, TEditDto> : CrudService<IRetrieveService, IEditService<TEditDto>, TRepository, TEntity, TEditDto>, IEditService<TEditDto>
        where TRepository : IRepository<TEntity>
        where TEntity : IEntity
    {
        #region Fields
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        #endregion

        #region Constructors
        protected CrudService(IMapper mapper, TRepository repository)
            : base(new RetrieveService<TRepository, TEntity>(mapper, repository), null)
        {
            _mapper = mapper;
            _repository = repository;
            
            var retrieveService = (RetrieveService<TRepository, TEntity>)_retrieveService;
            
            retrieveService.FindIncludes = FindIncludes;
            retrieveService.GetAllIncludes = GetAllIncludes;
        }
        #endregion

        #region Overrides
        
        #region IEditService
        public override Guid Create(TEditDto dto) => ((IEditService<TEditDto>)this).Create(dto);
        public override void Update(Guid id, TEditDto dto) => ((IEditService<TEditDto>)this).Update(id, dto);
        public override void Delete(Guid id) => ((IEditService<TEditDto>)this).Delete(id);
        #endregion
        #endregion

        #region IEditService
        Guid IEditService<TEditDto>.Create(TEditDto dto) => CreateAbstract(dto);

        void IEditService<TEditDto>.Delete(Guid id) => DeleteAbstract(id);

        void IEditService<TEditDto>.Update(Guid id, TEditDto dto) => UpdateAbstract(id, dto);
        #endregion

        #region Abstracts
        protected abstract Guid CreateAbstract(TEditDto dto);
        protected abstract void UpdateAbstract(Guid id, TEditDto dto);
        protected abstract void DeleteAbstract(Guid id);
        #endregion
    }

    public abstract class CrudService<TRetrieveService, TEditService, TRepository, TEntity, TEditDto> : IRetrieveService, IEditService<TEditDto>
        where TRetrieveService : IRetrieveService
        where TEditService : IEditService<TEditDto>
        where TRepository : IRepository<TEntity>
        where TEntity : IEntity
    {
        #region Campos
        protected readonly TRetrieveService _retrieveService;
        protected readonly TEditService _editService;
        #endregion

        #region Constructors
        protected CrudService(TRetrieveService retrieveService, TEditService editService)
        {
            _retrieveService = retrieveService;
            _editService = editService;
        }
        #endregion

        #region IRetrieveService
        public virtual TDto Find<TDto>(Guid id) => _retrieveService.Find<TDto>(id);
        public virtual IEnumerable<TDto> GetAll<TDto>() => _retrieveService.GetAll<TDto>();
        #endregion

        #region IEditService
        public virtual Guid Create(TEditDto dto) => _editService.Create(dto);
        public virtual void Update(Guid id, TEditDto dto) => _editService.Update(id, dto);
        public virtual void Delete(Guid id) => _editService.Delete(id);
        #endregion

        #region Virtuais
        protected virtual IEnumerable<Expression<Func<TEntity, object>>> GetAllIncludes
        {
            get
            {
                return FindIncludes;
            }
        }

        protected virtual IEnumerable<Expression<Func<TEntity, object>>> FindIncludes
        {
            get
            {
                return Enumerable.Empty<Expression<Func<TEntity, object>>>();
            }
        }
        #endregion
    }
}