using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence
{
    //all the logic to evaluate specifications will be here, the dynamic query building here
    internal static class SpecificationEvaluator 
    {
        //fun take two parameters: an IQueryable<Tentity> entryPoint representing the initial queryable collection of entities, and an ISpecifications<Tentity, TKey> specifications representing the specifications to be applied to the query.
        public static IQueryable<Tentity> CreateQuery<Tentity, TKey>(IQueryable<Tentity> entryPoint, ISpecifications<Tentity, TKey> specifications) 
                 where Tentity : BaseClass<TKey>
        {

            var query = entryPoint;
            if (specifications is not null)
            {
                if(specifications.Criteria is not null)
                {
                    query = query.Where(specifications.Criteria);
                }

                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                {
                    //Aggregate is used to apply multiple include expressions to the query by taking the initial query and successively applying each include expression to it.
                    // it take parameters: the current state of the query and the current include expression from the collection.
                    query = specifications.IncludeExpressions.Aggregate(query, (currentQuery, incudeExp) => currentQuery.Include(incudeExp));
                }

                if(specifications.orderBy is not null)
                {
                    query = query.OrderBy(specifications.orderBy);
                }

                if (specifications.orderByDescending is not null)
                {
                    query = query.OrderByDescending(specifications.orderByDescending);
                }

                if (specifications.isPaginated)
                {
                    query = query.Skip(specifications.skip).Take(specifications.take);
                }
            }
            return query;

        }

    }
}
