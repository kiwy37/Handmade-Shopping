using Daw.DataLayer.DataBaseConenction;
using Daw.DataLayer.Repositories;
using Daw.DataLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace Daw.Dependencies
{
    public class Dependencies
    {
        public static void Inject(WebApplicationBuilder app)
        {
            app.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(app.Configuration.GetConnectionString("DefaultConnection"),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            });
            app.Services.AddCors(options =>
            {
                options.AddPolicy("Access-Control-Allow-Origin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
            AddRepositories(app.Services);
            AddServices(app.Services);
        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<UserProductRepository>();
            services.AddScoped<UnitOfWork>();
        }
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<UserProductService>();
            services.AddScoped<ProductService>();
        }
    }
}
