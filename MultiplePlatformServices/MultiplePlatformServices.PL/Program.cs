
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MultiplePlatformServices.BLL.Services;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.Data;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Repository;
using MultiplePlatformServices.DAL.Repository.RepositoryInterfaces;
using MultiplePlatformServices.DAL.Utilities;
using System.Text;
using System.Threading.Tasks;

namespace MultiplePlatformServices.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            });




            //this to connect with Sql Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ISeedData,RoleSeedData>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? "TemporaryFallbackSuperSecretKey123456!!!"))
                };
            });

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


            builder.Services.AddHttpContextAccessor();


            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IServiceCategoryRepository, ServiceCategoryRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();


            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IServiceCategoryService, ServiceCategoryService>();
            builder.Services.AddScoped<IServiceService,ServiceService>();
            builder.Services.AddScoped<ICartService, CartService>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach(var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }
            app.Run();
        }
    }
}
