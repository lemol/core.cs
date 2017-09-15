using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Model;

namespace Application.Services
{
    public static class Helpers
    {
        public static IEnumerable<Expression<Func<TEntity, object>>> Includes<TEntity>(params Expression<Func<TEntity, object>>[] incs)
            where TEntity : IEntity => Core.Application.Services.Helpers.Includes<TEntity, Guid>(incs);
    }
}