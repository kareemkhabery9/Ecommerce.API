using Ecommerce.Domain;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.ProductModule;
using Ecommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Data.Data_Seed
{
    internal class DataIntializer : IDataIntializer
    {
        private readonly StoreDbContext _dbContext;

        public DataIntializer(StoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task IntailizeAsync()
        {

            try
            {
                //First, check if data already exists to avoid duplicate seeding
                var hasProducts = await _dbContext.Products.AnyAsync();
                var hasBrands = await _dbContext.ProductBrands.AnyAsync();
                var hasTypes = await _dbContext.ProductTypes.AnyAsync();

                if(hasProducts && hasBrands && hasTypes)
                    return;

                //Second, seed data in the correct order to satisfy foreign key constraints
                if (!hasBrands)
                {
                   await SeedDataFromJson<ProductBrand, int>("brands.json", _dbContext.ProductBrands);
                }

                if(!hasTypes)
                {
                   await SeedDataFromJson<ProductType, int>("types.json", _dbContext.ProductTypes);
                }
                // Save changes after seeding brands and types to ensure foreign key constraints are met before seeding products that depend on them
                await _dbContext.SaveChangesAsync();

                if(!hasProducts)
                {
                   await SeedDataFromJson<Product, int>("products.json", _dbContext.Products);
                   await _dbContext.SaveChangesAsync();
                }
            }



            catch(Exception ex)
            {
                throw new Exception($"An error occurred during data initialization: {ex.Message}", ex);
            }

        }


        // Generic method to seed data from a JSON file into the specified DbSet
        private async Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> dbSet) where T : BaseEntity<TKey>
        {
            var filePath = @"..\Ecommerce.Persistence\Data\Data Seed\JsonFiles\"+ fileName;

            if(!File.Exists(filePath))
                throw new FileNotFoundException($"The file {filePath} was not found.");


            try
            {
                var dataStream = File.OpenRead(filePath);

                var data =await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    // To handle case-insensitive property names in JSON
                    PropertyNameCaseInsensitive = true
                });


                if(data != null && data.Count > 0)
                {
                   await dbSet.AddRangeAsync(data);
                }

            }

            catch (Exception ex)
            {
                throw new Exception($"An error occurred while seeding data from {fileName}: {ex.Message}", ex);
            }









            }


    }
}
