using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseClass<TKey>
    {
      ICollection<Expression<Func<TEntity,object>>> IncludeExpressions {get;}

        Expression<Func<TEntity, bool>> Criteria {get;}

        Expression<Func<TEntity,object>> orderBy {get; }

        Expression<Func<TEntity,object>> orderByDescending {get; }

    }
}
