using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data.DbContexts;
using Ecommerce.Persistence.IdentityData.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ecommerce.API.Externals
{
    public static class WebApplicationRegister
    {

        //External Method to Migrate Database and call it in Program.cs
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
           await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            // I Can't use Any here because PendingMigrations is awaitable that mean it may be not be completed yet and it Enumerable data
            // note : Anyasync is used with IQueryable data not Enumerable data
            var PendingMigrations =await dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            return app;

        }


        public static async Task<WebApplication> MigrateIdentityDataBaseAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();

            // I Can't use Any here because PendingMigrations is awaitable that mean it may be not be completed yet and it Enumerable data
            // note : Anyasync is used with IQueryable data not Enumerable data
            var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
            {
                dbContext.Database.Migrate();
            }

            return app;

        }


        //External Method to Seed Data and call it in Program.cs
        //To take Object of IDataIntializer from DI Container and call IntailizeAsync method
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataIntializer>();
            await dataIntializer.IntailizeAsync();

            return app;
        }






    }
}
