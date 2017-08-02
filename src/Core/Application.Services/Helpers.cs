using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Domain.Model;

namespace Core.Application.Services
{
    public static class Helpers
    {
        public static IEnumerable<Expression<Func<TEntity, object>>> Includes<TEntity, TIdentity>(params Expression<Func<TEntity, object>>[] incs)
            where TEntity : IEntity<TIdentity> => incs;
    }
}