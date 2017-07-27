using System;
using Core.Domain.Model;
using Core.Domain.Data;

namespace Core.Application.Services
{
    public abstract class EditServiceBase<TRepository, TEntity, TIdentity, TEditDto> : IEditService<TIdentity, TEditDto>
        where TEntity : IEntity<TIdentity>
        where TRepository : IRepository<TEntity, TIdentity>
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
        public abstract TIdentity Create(TEditDto dto);
        public abstract void Update(TIdentity id, TEditDto dto);

        public virtual void Delete(TIdentity id)
        {
            var item = _repository.Find(id);
            _repository.Delete(item);
            _uow.Commit();
        }
        #endregion
    }

    public abstract class EditServiceBase<TEntity, TIdentity, TEditDto> : EditServiceBase<IRepository<TEntity, TIdentity>, TEntity, TIdentity, TEditDto>
        where TEntity : IEntity<TIdentity>
    {
        public EditServiceBase(IUnitOfWork uow, IRepository<TEntity, TIdentity> repository)
            : base(uow, repository)
        {

        }
    }
}