using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IUnitOfWork
    {
        //fun ction to save changes asynchronously to the database
       Task<int> SaveChangesAsync();

        //function to get a generic repository for a specific entity type
       IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseClass<TKey>;


    }
}
