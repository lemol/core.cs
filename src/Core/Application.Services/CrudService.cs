using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Infrastructure;
using Core.Domain.Model;
using Core.Domain.Data;

namespace Core.Application.Services
{
    public abstract class CrudService<TEntity, TIdentity, TEditDto> : CrudService<IRepository<TEntity, TIdentity>, TEntity, TIdentity, TEditDto, SimpleQuery>
        where TEntity : IEntity<TIdentity>
    {
        protected CrudService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity, TIdentity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }

    public abstract class CrudService<TEntity, TIdentity, TEditDto, TQuery> : CrudService<IRepository<TEntity, TIdentity>, TEntity, TIdentity, TEditDto, TQuery>
        where TEntity : IEntity<TIdentity>
    {
        protected CrudService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<TEntity, TIdentity> repository)
            : base(mapper, unitOfWork, repository)
        {
        }
    }

    public abstract class CrudService<TRepository, TEntity, TIdentity, TEditDto, TQuery> : CrudService<IRetrieveService<TIdentity, TQuery>, IEditService<TIdentity, TEditDto>, TRepository, TEntity, TIdentity, TEditDto, TQuery>, IEditService<TIdentity, TEditDto>
        where TRepository : IRepository<TEntity, TIdentity>
        where TEntity : IEntity<TIdentity>
    {
        #region Fields
        protected readonly IMapper _mapper;
        protected readonly TRepository _repository;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        protected CrudService(IMapper mapper, IUnitOfWork unitOfWork, TRepository repository)
            : base(new RetrieveService<TRepository, TEntity, TIdentity, TQuery>(mapper, repository), null)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
            
            var retrieveService = (RetrieveService<TRepository, TEntity, TIdentity, TQuery>)_retrieveService;
            
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
        protected virtual void DeleteAbstract(TIdentity id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
        }
        #endregion
    }

    public abstract class CrudService<TRetrieveService, TEditService, TRepository, TEntity, TIdentity, TEditDto, TQuery> : ICrudService<TIdentity, TEditDto, TQuery>
        where TRetrieveService : IRetrieveService<TIdentity, TQuery>
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
        public virtual IEnumerable<TDto> GetQuery<TDto>(TQuery query) => _retrieveService.GetQuery<TDto>(query);
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