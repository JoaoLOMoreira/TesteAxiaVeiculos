using AxiaVeiculosApplication.Common.Behaviors;
using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Queries;
using AxiaVeiculosApplication.Veiculos.Services;
using AxiaVeiculosApplication.Veiculos.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AxiaVeiculosApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IVeiculoService, VeiculoService>();

        services.AddScoped<IValidator<AdicionarVeiculoCommand>, AdicionarVeiculoCommandValidator>();
        services.AddScoped<IValidator<AtualizarVeiculoCommand>, AtualizarVeiculoCommandValidator>();
        services.AddScoped<IValidator<ExcluirVeiculoCommand>, ExcluirVeiculoCommandValidator>();
        services.AddScoped<IValidator<ObterVeiculoPorIdQuery>, ObterVeiculoPorIdQueryValidator>();

        return services;
    }
}
