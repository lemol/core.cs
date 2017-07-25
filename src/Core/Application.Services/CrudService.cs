using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lemolsoft.Framework.Infrastructure;
using Lemolsoft.Framework.Domain;
using Lemolsoft.Framework.Domain.Data;

namespace Lemolsoft.Framework.Application.Services
{
    public abstract class CrudService<TEntity, TIdentity, TEditDto> : CrudService<IRepository<TEntity, TIdentity>, TEntity, TIdentity, TEditDto>
        where TEntity : IEntity<TIdentity>
    {
        protected CrudService(IMapper mapper, IRepository<TEntity, TIdentity> repository)
            : base(mapper, repository)
        {
        }
    }

    public abstract class CrudService<TRepository, TEntity, TIdentity, TEditDto> : CrudService<IRetrieveService<TIdentity>, IEditService<TIdentity, TEditDto>, TRepository, TEntity, TIdentity, TEditDto>, IEditService<TIdentity, TEditDto>
        where TRepository : IRepository<TEntity, TIdentity>
        where TEntity : IEntity<TIdentity>
    {
        #region Fields
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        #endregion

        #region Constructors
        protected CrudService(IMapper mapper, TRepository repository)
            : base(new RetrieveService<TRepository, TEntity, TIdentity>(mapper, repository), null)
        {
            _mapper = mapper;
            _repository = repository;
            
            var retrieveService = (RetrieveService<TRepository, TEntity, TIdentity>)_retrieveService;
            
            retrieveService.FindIncludes = FindIncludes;
            retrieveService.GetAllIncludes = GetAllIncludes;
        }
        #endregion

        #region Overrides
        
        #region IEditService
        public override TIdentity Create(TEditDto dto) => ((IEditService<TIdentity, TEditDto>)this).Create(dto);
        public override void Update(TIdentity id, TEditDto dto) => ((IEditService<TIdentity, TEditDto>)this).Update(id, dto);
        public override void Delete(TIdentity id) => ((IEditService<TIdentity, TEditDto>)this).Delete(id);
        #endregion
        #endregion

        #region IEditService
        TIdentity IEditService<TIdentity, TEditDto>.Create(TEditDto dto) => CreateAbstract(dto);

        void IEditService<TIdentity, TEditDto>.Delete(TIdentity id) => DeleteAbstract(id);

        void IEditService<TIdentity, TEditDto>.Update(TIdentity id, TEditDto dto) => UpdateAbstract(id, dto);
        #endregion

        #region Abstracts
        protected abstract TIdentity CreateAbstract(TEditDto dto);
        protected abstract void UpdateAbstract(TIdentity id, TEditDto dto);
        protected abstract void DeleteAbstract(TIdentity id);
        #endregion
    }

    public abstract class CrudService<TRetrieveService, TEditService, TRepository, TEntity, TIdentity, TEditDto> : IRetrieveService<TIdentity>, IEditService<TIdentity, TEditDto>
        where TRetrieveService : IRetrieveService<TIdentity>
        where TEditService : IEditService<TIdentity, TEditDto>
        where TRepository : IRepository<TEntity, TIdentity>
        where TEntity : IEntity<TIdentity>
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
        public virtual TDto Find<TDto>(TIdentity id) => _retrieveService.Find<TDto>(id);
        public virtual IEnumerable<TDto> GetAll<TDto>() => _retrieveService.GetAll<TDto>();
        #endregion

        #region IEditService
        public virtual TIdentity Create(TEditDto dto) => _editService.Create(dto);
        public virtual void Update(TIdentity id, TEditDto dto) => _editService.Update(id, dto);
        public virtual void Delete(TIdentity id) => _editService.Delete(id);
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