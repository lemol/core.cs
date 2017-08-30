using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Data;
using Core.Domain.Model;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Domain.Data.EntityFramework
{
    public abstract class RepositoryBase<TDbContext, TEntity, TIdentity> : IRepository<TEntity, TIdentity>
        where TEntity : class, IEntity<TIdentity>
        where TDbContext : DbContext
    {
        protected readonly TDbContext _db;
        protected readonly DbSet<TEntity> _dbSet;
        protected RepositoryBase(TDbContext db)
        {
            _db = db;
            _dbSet = db.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            var dbEntityEntry = _db.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
                _dbSet.Add(entity);
        }

        public TEntity Find(TIdentity id)
        {
            return Query()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> Query()
        {
            return _dbSet;
        }

        [Obsolete]
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] include)
        {
            return include.Aggregate(_dbSet.AsQueryable(), (acc, act) => acc.Include(ToIncludeString(act)));
        }

        private string ToIncludeString(Expression<Func<TEntity, object>> expr)
        {
            var res = ExpressionHelper.GetExpressionText(expr);
            return res;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            var dbEntityEntry = _db.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }
    }

    public static class ExpressionHelper
    {
        public static string GetExpressionText(string expression)
        {
            // If it's exactly "model", then give them an empty string, to replicate the lambda behavior.
            return
                string.Equals(expression, "model", StringComparison.OrdinalIgnoreCase)
                    ? string.Empty
                    : expression;
        }

        public static string GetExpressionText(LambdaExpression expression)
        {
            // Split apart the expression string for property/field accessors to create its name
            var nameParts = new Stack<string>();
            var part = expression.Body;

            while (part != null)
            {
                if (part.NodeType == ExpressionType.Call)
                {
                    var methodExpression = (MethodCallExpression)part;

                    if (!IsSingleArgumentIndexer(methodExpression))
                    {
                        break;
                    }

                    part = methodExpression.Object;
                }
                else if (part.NodeType == ExpressionType.ArrayIndex)
                {
                    var binaryExpression = (BinaryExpression)part;

                    part = binaryExpression.Left;
                }
                else if (part.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpressionPart = (MemberExpression)part;
                    nameParts.Push("." + memberExpressionPart.Member.Name);
                    part = memberExpressionPart.Expression;
                }
                else if (part.NodeType == ExpressionType.Parameter)
                {
                    // When the expression is parameter based (m => m.Something...), we'll push an empty
                    // string onto the stack and stop evaluating. The extra empty string makes sure that
                    // we don't accidentally cut off too much of m => m.Model.
                    nameParts.Push(string.Empty);
                    part = null;
                }
                else
                {
                    break;
                }
            }

            // If it starts with "model", then strip that away
            if (nameParts.Count > 0 && string.Equals(nameParts.Peek(), ".model", StringComparison.OrdinalIgnoreCase))
            {
                nameParts.Pop();
            }

            if (nameParts.Count > 0)
            {
                return nameParts.Aggregate((left, right) => left + right).TrimStart('.');
            }

            return string.Empty;
        }

        public static bool IsSingleArgumentIndexer(Expression expression)
        {
            var methodExpression = expression as MethodCallExpression;
            if (methodExpression == null || methodExpression.Arguments.Count != 1)
            {
                return false;
            }

            // Check whether GetDefaultMembers() (if present in CoreCLR) would return a member of this type. Compiler
            // names the indexer property, if any, in a generated [DefaultMember] attribute for the containing type.
            var declaringType = methodExpression.Method.DeclaringType;
            var defaultMember = declaringType.GetTypeInfo().GetCustomAttribute<DefaultMemberAttribute>(inherit: true);
            if (defaultMember == null)
            {
                return false;
            }

            // Find default property (the indexer) and confirm its getter is the method in this expression.
            return declaringType.GetRuntimeProperties().Any(
                property => (string.Equals(defaultMember.MemberName, property.Name, StringComparison.Ordinal) &&
                    property.GetMethod == methodExpression.Method));
        }
    }
}