using AxiaVeiculosApplication;
using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Queries;
using AxiaVeiculosDomain.Enumerators;
using AxiaVeiculosDomain.Repositories;
using AxiaVeiculosInfra.Data;
using AxiaVeiculosInfra.Repositories;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AxiaVeiculosTests.Application.MediatR;

public sealed class VeiculosMediatRFlowTests
{
    [Fact]
    public async Task SendAsync_ComandoValido_DeveCadastrarEListarVeiculo()
    {
        using var serviceProvider = CriarServiceProvider();
        var sender = serviceProvider.GetRequiredService<ISender>();

        var veiculoCriado = await sender.Send(new AdicionarVeiculoCommand(
            "Honda Civic EXL",
            MarcaVeiculo.Honda,
            "Civic",
            "Cambio automatico",
            145000m));

        var veiculos = await sender.Send(new ListarVeiculosQuery());

        veiculos.Should().ContainSingle(veiculo => veiculo.Id == veiculoCriado.Id);
    }

    [Fact]
    public async Task SendAsync_ComandoInvalido_DeveExecutarValidacaoDoPipeline()
    {
        using var serviceProvider = CriarServiceProvider();
        var sender = serviceProvider.GetRequiredService<ISender>();

        var act = () => sender.Send(new AdicionarVeiculoCommand(
            string.Empty,
            MarcaVeiculo.Honda,
            string.Empty,
            null,
            -1m));

        var exception = await act.Should().ThrowAsync<ValidationException>();
        exception.Which.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain(new[]
            {
                "A descricao e obrigatoria.",
                "O modelo e obrigatorio.",
                "O valor nao pode ser negativo."
            });
    }

    private static ServiceProvider CriarServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddApplication();
        services.AddDbContext<VeiculosDbContext>(options =>
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        services.AddScoped<IVeiculoRepository, VeiculoRepository>();

        return services.BuildServiceProvider();
    }
}
