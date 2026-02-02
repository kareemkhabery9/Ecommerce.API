
using Ecommerce.API.Externals;
using Ecommerce.Domain.Contracts;
using Ecommerce.Persistence.Data.Data_Seed;
using Ecommerce.Persistence.Data.DbContexts;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Services;
using Ecommerce.Services.Abstraction;
using Ecommerce.Services.MappingProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Ecommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Registration DI container
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IDataIntializer, DataIntializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);

          
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });


            #endregion

            var app = builder.Build();

            await app.MigrateDataBaseAsync();

            await app.SeedDataAsync();

            #region Configure PipeLine [MiddleWare]
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
