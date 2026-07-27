
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiplePlatformServices.BLL.Services;
using MultiplePlatformServices.BLL.Services.Interfaces;
using MultiplePlatformServices.DAL.Data;
using MultiplePlatformServices.DAL.Models;
using MultiplePlatformServices.DAL.Utilities;
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
            builder.Services.AddOpenApi();




            //this to connect with Sql Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<ISeedData,RoleSeedData>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

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
