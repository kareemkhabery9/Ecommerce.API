using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
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

        #region Filteration
        public Expression<Func<TEntity, bool>> Criteria { get; }
        #endregion


        #region Including

        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        //Fun to add all include expressions to a list of expressions
        protected void AddInclude(Expression<Func<TEntity, object>> includeExp)
        {
            IncludeExpressions.Add(includeExp);
        }
        #endregion


        #region Ordering

        public Expression<Func<TEntity, object>> orderBy { private set; get; }

        public Expression<Func<TEntity, object>> orderByDescending { private set; get; }


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp)
        {
            orderBy = orderByExp;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp)
        {
            orderByDescending = orderByDescExp;

        }
        #endregion


        #region Pagination

        public int skip { private set; get; }

        public int take { private set; get; }

        public bool isPaginated { private set; get; }

        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            isPaginated = true;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
        }


        #endregion



    }
}
