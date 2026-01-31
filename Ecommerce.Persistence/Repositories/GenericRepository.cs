using Ecommerce.Domain;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseClass<TKey>
    {

        private readonly StoreDbContext _dbContext;
        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }


        public async Task AddAsync(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications)
                         .CountAsync();
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync() 
                        => await _dbContext.Set<TEntity>().ToListAsync();



        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
           var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(),  specifications);

            return await query.ToListAsync();
        }



        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications);
            
            return await query.FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
           _dbContext.Set<TEntity>().Update(entity);
        }

     
    }
}
