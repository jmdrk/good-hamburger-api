using GoodHamburger.Application.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace GoodHamburger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}