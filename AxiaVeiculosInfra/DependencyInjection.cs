using AxiaVeiculosDomain.Repositories;
using AxiaVeiculosInfra.Data;
using AxiaVeiculosInfra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiaVeiculosInfra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services)
    {
        services.AddDbContext<VeiculosDbContext>(options => options.UseInMemoryDatabase("AxiaVeiculos"));

        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        return services;
    }

    public static async Task EnsureDatabaseCreatedAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<VeiculosDbContext>();

        await context.Database.EnsureCreatedAsync();
    }
}
