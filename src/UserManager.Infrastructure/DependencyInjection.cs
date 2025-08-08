using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManager.Infrastructure.Repositories.Interfaces;

namespace UserManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.Scan(r => r.FromAssembliesOf(typeof(IRepository<>))
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith("Repository")))
                .AsMatchingInterface()
                .WithLifetime(ServiceLifetime.Scoped));

            return services;
        }
    }
}