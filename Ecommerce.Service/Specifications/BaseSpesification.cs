using Ecommerce.Domain;
using Ecommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications
{
    internal abstract class BaseSpesification<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseClass<TKey>
    {
        protected BaseSpesification(Expression<Func<TEntity, bool>> criteriaExp)
        {
            Criteria = criteriaExp;
        }
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; }


        //Fun to add all include expressions to a list of expressions
        protected void AddInclude( Expression<Func<TEntity,object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }


    }
}
