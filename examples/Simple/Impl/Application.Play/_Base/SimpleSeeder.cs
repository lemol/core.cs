using System;
using Domain.Data;
using Domain.Model;

namespace Application.Play
{
    public class SimpleSeeder<TEntity>
        where TEntity : IEntity
    {
        #region Fields
        protected readonly IRepository<TEntity> _repository;
        #endregion

        #region Constructores
        public SimpleSeeder(IRepository<TEntity> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Interface
        public virtual void Seed()
        {
        }

        public virtual void SeedWith(Action<IRepository<TEntity>> seederAct) => seederAct(_repository);
        #endregion
    }
}