using GoodHamburger.Domain.Repositories;
using GoodHamburger.Infrastructure.Persistence;
using GoodHamburger.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<GoodHamburgerDbContext>(options =>
        {
            options.UseInMemoryDatabase("GoodHamburgerDb");
        });

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}