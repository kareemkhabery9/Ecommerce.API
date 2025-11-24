using Ecommerce.Domain;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        private readonly Dictionary<Type, object> _repositories = [];

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseClass<TKey>
        {
            //to get the type of entity
            var entityType = typeof(TEntity);

            //check if the repository already exists
            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }

            //if not, create a new repository
            var newRepo = new GenericRepository<TEntity, TKey>(_dbContext);

            _repositories[entityType] = newRepo;

            return newRepo;
        }


        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    }
}
