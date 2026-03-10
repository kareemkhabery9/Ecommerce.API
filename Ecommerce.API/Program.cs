
using Ecommerce.API.CustomMiddleware;
using Ecommerce.API.Externals;
using Ecommerce.API.Factories;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.IdentityModule;
using Ecommerce.Persistence.Data.Data_Seed;
using Ecommerce.Persistence.Data.DbContexts;
using Ecommerce.Persistence.IdentityData.DataSeed;
using Ecommerce.Persistence.IdentityData.DbContexts;
using Ecommerce.Persistence.Repositories;
using Ecommerce.Services;
using Ecommerce.Services.Abstraction;
using Ecommerce.Services.MappingProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

            builder.Services.AddKeyedScoped<IDataIntializer, DataIntializer>("Default");
            builder.Services.AddKeyedScoped<IDataIntializer, IdentityDataIntializer>("Identity");

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(ServiceAssemblyReference).Assembly);

          
            builder.Services.AddScoped<IProductService, ProductService>();

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddScoped<ICacheRepository,CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiVaildationResponse;
            });



            builder.Services.AddDbContext<StoreIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //give all managers (userManger - roleManager - signInManager)
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<StoreIdentityDbContext>();


            //give what i need here only userManger and roleManger 
            builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddRoleManager<RoleManager<IdentityRole>>();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                  
                    ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                    ValidAudience = builder.Configuration["JwtOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]!))

                };

            });

            #endregion

            var app = builder.Build();

            await app.MigrateDataBaseAsync();
            await app.MigrateIdentityDataBaseAsync();

            await app.SeedDataAsync();
            await app.SeedIdentityDataAsync();

            #region Configure PipeLine [MiddleWare]
            // Configure the HTTP request pipeline.
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            await app.RunAsync();
        }
    }
}
