using System;
using Lemolsoft.Framework.Domain;
using Lemolsoft.Framework.Domain.Data;

namespace Lemolsoft.Framework.Application.Services
{
    public abstract class EditServiceBase<TRepository, TEntity, TEditDto> : IEditService<TEditDto>
        where TEntity : IEntity
        where TRepository : IRepository<TEntity>
    {
        #region Field
        protected readonly IUnitOfWork _uow;
        protected readonly TRepository _repository;
        #endregion

        #region Constructors
        protected EditServiceBase(IUnitOfWork uow, TRepository repository)
        {
            _uow = uow;
            _repository = repository;
        }
        #endregion

        #region IEditService
        public abstract Guid Create(TEditDto dto);
        public abstract void Update(Guid id, TEditDto dto);

        public virtual void Delete(Guid id)
        {
            var item = _repository.Find(id);
            _repository.Delete(item);
            _uow.Commit();
        }
        #endregion
    }

    public abstract class EditServiceBase<TEntity, TEditDto> : EditServiceBase<IRepository<TEntity>, TEntity, TEditDto>
        where TEntity : IEntity
    {
        public EditServiceBase(IUnitOfWork uow, IRepository<TEntity> repository)
            : base(uow, repository)
        {

        }
    }
}