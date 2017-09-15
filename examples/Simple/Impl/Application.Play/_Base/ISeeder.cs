using System;
using Domain.Data;
using Domain.Model;

namespace Application.Play
{
    public interface ISeeder<TEntity>
        where TEntity : IEntity
    {
        void Seed();
        void SeedWith(Action<IRepository<TEntity>> seederAct);
    }
}